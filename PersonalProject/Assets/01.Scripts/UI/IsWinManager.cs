using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IsWinManager : MonoBehaviour
{

    public static IsWinManager instance;

    private Label _win;


    private UIDocument _uiDocument;

    private void Awake()
    {
        instance = this;
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        var root = _uiDocument.rootVisualElement;

        _win = root.Q<Label>("congratuation-lbl");
    }

    public void isWin()
    {
        Color origin = _win.style.color.value;
        _win.style.color = new StyleColor(new Color(origin.r, origin.g, origin.b, 255));
    }

}