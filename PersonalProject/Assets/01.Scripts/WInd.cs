using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WInd : MonoBehaviour
{
    private void Awake()
    {
        MainWindow1.instance.StartFade();
    }
}
