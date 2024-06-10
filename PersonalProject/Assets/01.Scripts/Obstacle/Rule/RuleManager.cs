using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleManager : MonoBehaviour
{
    public static RuleManager instance;

    [SerializeField] private Babo _babo;
    [SerializeField] private Rock _rock;
    [SerializeField] private Is _is;

    private void Awake()
    {
        instance = this;
    }
}
