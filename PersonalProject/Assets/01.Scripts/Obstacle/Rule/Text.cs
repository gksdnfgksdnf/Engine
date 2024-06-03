using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text : MonoBehaviour
{
    public string textType;
    void Start()
    {
        gameObject.tag = "Text";
        gameObject.name = textType;
    }

}
