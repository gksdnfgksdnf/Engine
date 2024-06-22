using UnityEngine;

public class Subject : MonoBehaviour
{
    [SerializeField] private LayerMask _whatisTarget;
    [SerializeField] private GameObject[] _subjectObj;

    private Vector3[] _directions = { Vector3.back, Vector3.right };
    private int _range = 1;

    private bool _allRaysMissed = false;
    private bool _actioned = false;

    void FixedUpdate()
    {
        DrawRays();
        if (_allRaysMissed && !_actioned)
            _actioned = true;
    }

    private void DrawRays()
    {
        _allRaysMissed = true;

        for (int i = 0; i < _directions.Length; i++)
        {
            Vector3 direction = _directions[i];
            bool isHit = Physics.Raycast(transform.position, direction, out RaycastHit hit, _range, _whatisTarget);

            if (isHit)
            {
                _allRaysMissed = false; // 하나라도 감지되면 false로 설정
                _actioned = false;

                if (hit.collider.TryGetComponent(out Is Is))
                    Is.DrawRay(direction, _subjectObj);


            }
        }
    }
}
