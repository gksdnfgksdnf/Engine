using UnityEngine;

public class Subject : MonoBehaviour
{
    [SerializeField] private LayerMask _whatisTarget;

    [SerializeField] private GameObject _subjectObj;

    private Vector3[] _directions = { Vector3.forward, Vector3.back, Vector3.right, Vector3.left };
    private int _range = 1;

    void FixedUpdate()
    {
        DrawRays();
    }

    private void DrawRays()
    {
        for (int i = 0; i < _directions.Length; i++)
        {
            Vector3 direction = _directions[i];
            bool isHit = Physics.Raycast(transform.position, direction, out RaycastHit hit, _range, _whatisTarget);

            if (isHit)
            {
                if (hit.collider.TryGetComponent(out Is Is))
                    Is.DrawRay(direction, _subjectObj);

            }
        }
    }
}
