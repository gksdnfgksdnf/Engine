using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingToggle : MonoBehaviour
{
    private UIDocument _uiDocument;

    private Toggle _sound;

    private VisualElement _soundVisual;

    private Label _soundLabel;

    [SerializeField]private Font _font;
    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        var root = _uiDocument.rootVisualElement.Q<VisualElement>("settings");

        _sound = root.Q<Toggle>("sound-toggle");


        _soundLabel = _sound.Q<Label>();
        _soundVisual = _sound.Q<VisualElement>();

        _soundLabel.style.fontSize = 30;
        _soundLabel.style.unityFont = _font;
        _soundLabel.style.color = Color.white;

    }
}
