using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public static event Action<bool> onComplete;

    private Color startColor;


    private void Awake()
    {
        Instance = this;
    }


}
