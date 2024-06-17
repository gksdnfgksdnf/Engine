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
    private Label _settings;
    private Label _exitGame;
    private event Action<bool> onComplete;
    private Color startColor;

    private Label _quit;

    private VisualElement _real;
    private Label _realQuit;
    private Label _realExit;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        onComplete += HandleOnComplete;
    }

    private void OnEnable()
    {
        var root = _uiDocument.rootVisualElement;

        _fade = root.Q<VisualElement>("fade");
        _fade.pickingMode = PickingMode.Ignore;
        _startGame = root.Q<Label>("start-game");

        _settings = root.Q<Label>("settings");
        _settings.style.display = DisplayStyle.None;

        _exitGame = root.Q<Label>("exit-game");

        _quit = root.Q<Label>("quit");
        _realQuit = _real.Q<Label>("real-quit");
        _realExit = _real.Q<Label>("real-exit");



        _startGame.RegisterCallback<ClickEvent>(evt =>
        {
            StartCoroutine(FadeIn(_fade, 2f, true));
        });

        _settings.RegisterCallback<ClickEvent>(evt =>
        {
            _settings.style.display = DisplayStyle.Flex;
        });

        _exitGame.RegisterCallback<ClickEvent>(evt =>
        {
            StartCoroutine(FadeIn(_fade, 2f, false));
        });



        _quit.RegisterCallback<ClickEvent>(evt =>
        {
            _settings.style.display = DisplayStyle.None;
        });

        _realQuit.RegisterCallback<ClickEvent>(evt =>
        {
            Application.Quit();
        });

        _realExit.RegisterCallback<ClickEvent>(evt =>
        {
            _real.style.display = DisplayStyle.None;
            _settings.style.display = DisplayStyle.Flex;
        });

    }

    private void SetBackgroundColor(VisualElement element, Color color)
    {
        element.style.backgroundColor = color;
    }

    private IEnumerator FadeIn(VisualElement element, float duration, bool isStart)
    {
        startColor = element.resolvedStyle.backgroundColor;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            // 배경색의 alpha 값을 변화시킵니다.
            Color newColor = new(startColor.r, startColor.g, startColor.b, Mathf.Lerp(startColor.a, 1f, t));
            SetBackgroundColor(element, newColor);
            yield return null;
        }

        // 최종적으로 완전히 불투명한 배경 설정
        SetBackgroundColor(element, new Color(startColor.r, startColor.g, startColor.b, 1f));

        onComplete?.Invoke(isStart);
    }

    private void HandleOnComplete(bool isStart)
    {
        if (isStart)
            SceneManager.LoadScene(1); // 게임 시작
        else
            Application.Quit(); // 게임 종료
    }
}
