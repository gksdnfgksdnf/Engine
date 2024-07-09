using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class IsWinManager : MonoBehaviour
{
    public static IsWinManager instance;

    private bool _isWin = false;

    private Label _win;

    private Color _winColor;

    private UIDocument _uiDocument;

    private void Awake()
    {
        instance = this;
        _uiDocument = GetComponent<UIDocument>();
    }

    private void Start()
    {
        _winColor = new Color(1, 1, 0, 0);
    }
    private void OnEnable()
    {
        var root = _uiDocument.rootVisualElement;

        _win = root.Q<Label>("congratuation-lbl");
        _win.style.color = new StyleColor(_winColor);
    }

    public void IsWin()
    {
        _win.style.color = new StyleColor(new Color(_winColor.r, _winColor.g, _winColor.b, 1)); // 나타내기
        _isWin = true;

        MainWindow1.instance.StartFade();
    }

    public bool Wined()
    {
        return _isWin;
    }
}
