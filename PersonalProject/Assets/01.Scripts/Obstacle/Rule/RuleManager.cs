using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleManager : MonoBehaviour
{
    public static RuleManager instance;

    private void Awake()
    {
        instance = this;
    }
}
