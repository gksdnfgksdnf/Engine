using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public static event Action<bool> onComplete;

    private Color startColor;


    private void Awake()
    {
        Instance = this;
    }

    private void SetBackgroundColor(VisualElement element, Color color)
    {
        element.style.backgroundColor = color;
    }

    public IEnumerator FadeIn(VisualElement element, float duration, bool isStart)
    {
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

        onComplete?.Invoke(isStart);
    }
}
