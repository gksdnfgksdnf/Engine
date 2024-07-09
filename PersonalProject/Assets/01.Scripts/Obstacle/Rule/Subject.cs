using System.Collections;
using UnityEngine;

public class Subject : MonoBehaviour
{
    [SerializeField] private LayerMask _whatisTarget;
    [SerializeField] private GameObject[] _subjectObj;

    private Is _target;

    private Vector3[] _directions = { Vector3.back, Vector3.right };
    private int _range = 1;

    bool anyRayHit;

    private void Start()
    {
        DrawRays();
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.W))
            StartCoroutine(DrawRays());
        else if (Input.GetKeyDown(KeyCode.S))
            StartCoroutine(DrawRays());
        else if (Input.GetKeyDown(KeyCode.A))
            StartCoroutine(DrawRays());
        else if (Input.GetKeyDown(KeyCode.D))
            StartCoroutine(DrawRays());


    }

    private IEnumerator DrawRays()
    {
        yield return new WaitForSeconds(.5f);
        anyRayHit = false;

        for (int i = 0; i < _directions.Length; i++)
        {
            Vector3 direction = _directions[i];
            bool isHit = Physics.Raycast(transform.position, direction, out RaycastHit hit, _range, _whatisTarget);

            if (isHit)
            {
                anyRayHit = true;
                if (hit.collider.TryGetComponent(out Is Is))
                {
                    _target = Is;
                    _target.DrawRay(direction, _subjectObj);
                }
            }
        }

        if (!anyRayHit)
        {
            HandleAllRaysMissed();
        }
    }

    private void HandleAllRaysMissed()
    {
        foreach (GameObject obj in _subjectObj)
        {
            if (_target != null)
            {
                _target.DestroyComponent(obj);
            }
        }
    }
}
