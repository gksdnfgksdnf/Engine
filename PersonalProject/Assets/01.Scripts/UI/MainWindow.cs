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
        _exitGame = root.Q<Label>("exit-game");

        _startGame.RegisterCallback<ClickEvent>(evt =>
        {
            StartCoroutine(FadeIn(_fade, 2f, true));
        });

        _settings.RegisterCallback<ClickEvent>(evt =>
        {
            // Settings Ŭ�� �� �ʿ��� �۾� �߰�
        });

        _exitGame.RegisterCallback<ClickEvent>(evt =>
        {
            StartCoroutine(FadeIn(_fade, 2f, false));
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
            // ������ alpha ���� ��ȭ��ŵ�ϴ�.
            Color newColor = new(startColor.r, startColor.g, startColor.b, Mathf.Lerp(startColor.a, 1f, t));
            SetBackgroundColor(element, newColor);
            yield return null;
        }

        // ���������� ������ �������� ��� ����
        SetBackgroundColor(element, new Color(startColor.r, startColor.g, startColor.b, 1f));

        onComplete?.Invoke(isStart);
    }

    private void HandleOnComplete(bool isStart)
    {
        if (isStart)
            SceneManager.LoadScene(1); // ���� ����
        else
            Application.Quit(); // ���� ����
    }
}
