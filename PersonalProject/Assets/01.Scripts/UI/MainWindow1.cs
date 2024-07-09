using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainWindow1 : MonoBehaviour
{
    public static MainWindow1 instance;
    private UIDocument _uiDocument;

    private VisualElement _fade;
    private Color startColor;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        instance = this;
    }

    private void OnEnable()
    {
        var root = _uiDocument.rootVisualElement;


        _fade = root.Q<VisualElement>("fade");
        _fade.style.display = DisplayStyle.None;



    }

    private void SetBackgroundColor(VisualElement element, Color color)
    {
        element.style.backgroundColor = color;
    }

    private void HandleOnComplete()
    {
        SceneManager.LoadScene(2);
    }


    public IEnumerator FadeIn(VisualElement element, float duration)
    {
        yield return new WaitForSeconds(.5f);
        element.style.display = DisplayStyle.Flex;

        startColor = element.resolvedStyle.backgroundColor;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);

            Color newColor = new(startColor.r, startColor.g, startColor.b, Mathf.Lerp(startColor.a, 1f, t));
            SetBackgroundColor(element, newColor);
            yield return null;
        }

        // 최종적으로 완전히 불투명한 배경 설정
        SetBackgroundColor(element, new Color(startColor.r, startColor.g, startColor.b, 1f));

        HandleOnComplete();

    }

    public void StartFade()
    {
        StartCoroutine(FadeIn(_fade, 2));
    }
}
