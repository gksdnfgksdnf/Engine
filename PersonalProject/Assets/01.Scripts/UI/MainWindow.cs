using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System;

public class MainWindow : MonoBehaviour
{
    private UIDocument _uiDocument;

    private VisualElement _main;
    private VisualElement _fade;
    private Label _startGame;
    private Label _settingLabel;
    private VisualElement _settings;
    private Label _exitGame;
    private Color startColor;

    private VisualElement _quit;
    private VisualElement _quitContent;
    private Label _leave;
    private Label _exit;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        UIManager.onComplete += HandleOnComplete;
    }

    private void OnEnable()
    {
        var root = _uiDocument.rootVisualElement;

        _main = root.Q<VisualElement>("mainscreen");

        _fade = root.Q<VisualElement>("fade");
        _fade.style.display = DisplayStyle.None;

        _startGame = root.Q<Label>("start-game");

        _settingLabel = root.Q<Label>("settings-lbl");
        _settings = root.Q<VisualElement>("settings");

        _exitGame = root.Q<Label>("exit-game");

        _quit = root.Q<VisualElement>("quit");
        _quitContent = root.Q<VisualElement>("quit-content");

        _leave = _quitContent.Q<Label>("yes");
        _exit = _quitContent.Q<Label>("no");



        _startGame.RegisterCallback<ClickEvent>(evt =>
        {
            UIManager.Instance.FadeIn(_fade, 2f, true);
        });

        _settingLabel.RegisterCallback<ClickEvent>(evt =>
        {
            _settings.style.display = DisplayStyle.Flex;
            _main.style.display = DisplayStyle.None;

        });

        _exitGame.RegisterCallback<ClickEvent>(evt =>
        {
            _quit.style.display = DisplayStyle.Flex;
            _quitContent.style.top = new Length(0, LengthUnit.Percent);
        });

        _leave.RegisterCallback<ClickEvent>(evt =>
        {
            UIManager.Instance.FadeIn(_fade, 2f, false);
        });

        _exit.RegisterCallback<ClickEvent>(evt =>
        {
            _quitContent.style.top = new Length(-100, LengthUnit.Percent);
            StartCoroutine(ExitCallback());
        });

    }

    private IEnumerator ExitCallback()
    {
        yield return new WaitForSeconds(.5f);
        _quit.style.display = DisplayStyle.None;
    }

    private void SetBackgroundColor(VisualElement element, Color color)
    {
        element.style.backgroundColor = color;
    }

    private void HandleOnComplete(bool isStart)
    {
        if (isStart)
            SceneManager.LoadScene(1);
        else
            Application.Quit();
    }
}
