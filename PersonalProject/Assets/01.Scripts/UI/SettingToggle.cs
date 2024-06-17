using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingToggle : MonoBehaviour
{
    private UIDocument _uiDocument;

    private VisualElement _settings;

    private Toggle _total;
    private Toggle _bgm;
    private Toggle _effect;

    private Label _exit;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        var root = _uiDocument.rootVisualElement;

        _settings = root.Q<VisualElement>("settings");

        _exit = root.Q<Label>("exit");

        _total = root.Q<Toggle>("total-sound-toggle");
        _bgm = root.Q<Toggle>("bgm-toggle");
        _effect = root.Q<Toggle>("effect-sound-toggle");


        _exit.RegisterCallback<ClickEvent>(evt =>
        {
            _settings.style.display = DisplayStyle.None;
        });


        _total.RegisterValueChangedCallback(evt =>
        {
            if (evt.newValue)
                Debug.Log("total Toggle is on");
            else
                Debug.Log("total Toggle is off");
        });

        _bgm.RegisterValueChangedCallback(evt =>
        {
            if (evt.newValue)
                Debug.Log("bgm Toggle is on");
            else
                Debug.Log("bgm Toggle is off");
        });

        _effect.RegisterValueChangedCallback(evt =>
        {
            if (evt.newValue)
                Debug.Log("effect Toggle is on");
            else
                Debug.Log("effect Toggle is off");
        });




    }
}
