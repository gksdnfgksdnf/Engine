using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] float _range = 2f;
    [SerializeField] private LayerMask _whatis;
    public bool _is;

    void Start()
    {
        gameObject.tag = "Rock";        
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
}
