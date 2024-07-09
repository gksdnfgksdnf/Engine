using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    private void Awake()
    {
        int layer = LayerMask.NameToLayer("Wall");
        gameObject.layer = layer;
    }

    private void OnDestroy()
    {
        int layer = LayerMask.NameToLayer("Default");
        gameObject.layer = layer;
    }
}
