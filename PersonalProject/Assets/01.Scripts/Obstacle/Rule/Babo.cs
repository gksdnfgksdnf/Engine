using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Babo : MonoBehaviour
{
    [SerializeField] float _range = 2f;
    [SerializeField] private LayerMask _whatis;

    public bool _is;

    private void Start()
    {
        gameObject.tag = "Babo";
    }

    private void Update()
    {
        DrawRay();
    }

    void DrawRay()
    {
        bool isHitHorizontal = Physics.Raycast(transform.position + new Vector3(1, 0, 0), Vector3.left, out RaycastHit hitHorizontal, 2, _whatis);
        bool isHitVertical = Physics.Raycast(transform.position + new Vector3(0, 0, 1), Vector3.back, out RaycastHit hitVertical, 2, _whatis);

        if (isHitHorizontal || isHitVertical)
        {
            _is = true;
        }
        else
        {
            _is = false;
        }
    }

    public bool Is()
    {
        return _is;
    }

    private void OnDrawGizmos()
    {
        bool isHitHorizontal = Physics.Raycast(transform.position + new Vector3(1, 0, 0), Vector3.left, out RaycastHit hitHorizontal, 2, _whatis);
        bool isHitVertical = Physics.Raycast(transform.position + new Vector3(0, 0, 1), Vector3.back, out RaycastHit hitVertical, 2, _whatis);

        if (isHitHorizontal)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position + new Vector3(1, 0, 0), Vector3.left * hitHorizontal.distance);
            Gizmos.DrawSphere(hitHorizontal.point, 0.1f);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position + new Vector3(1, 0, 0), Vector3.left * 2);
        }

        if (isHitVertical)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position + new Vector3(0, 0, 1), Vector3.back * hitVertical.distance);
            Gizmos.DrawSphere(hitVertical.point, 0.1f);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position + new Vector3(0, 0, 1), Vector3.back * 2);
        }
    }
}
