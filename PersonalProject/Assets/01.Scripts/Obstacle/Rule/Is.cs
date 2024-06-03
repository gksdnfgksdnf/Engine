using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Is : MonoBehaviour
{
    private Babo babo;
    [SerializeField] float _range = 2f;
    [SerializeField] private LayerMask _whatisTarget;
    [SerializeField] bool _you;
    [SerializeField] private bool _rock;
    [SerializeField] private bool _push;

    private void Update()
    {
        bool isHitHorizontal = Physics.Raycast(transform.position + new Vector3(1, 0, 0), Vector3.left, out RaycastHit hitHorizontal, 2, _whatisTarget);
        bool isHitVertical = Physics.Raycast(transform.position + new Vector3(0, 0, 1), Vector3.back, out RaycastHit hitVertical, 2, _whatisTarget);

        if (isHitHorizontal)
        {
            Check(hitHorizontal);
        }
        else
        {
            _you = false;
            _push = false;

        }

        if (isHitVertical)
        {
            Check(hitVertical);
        }
        else
        {
            _you = false;
            _push = false;

        }
    }

    public bool Rock()
    {
        return _rock;
    }

    public bool You()
    {
        return _you;
    }

    public bool Push()
    {
        return _push;
    }

    void Check(RaycastHit hit)
    {
        if (hit.collider.gameObject.tag == "You")
            _you = true;
        else
            _you = false;

        if (hit.collider.gameObject.tag == "Rock")
            _rock = true;
        else
            _rock = false;

        if (hit.collider.gameObject.tag == "Push")
            _push = true;
        else
            _push = false;
    }
}
