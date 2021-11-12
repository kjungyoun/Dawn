using UnityEngine;
using System.Collections;

public class Twinkle : MonoBehaviour
{
    private float fadeTime = 1.0f;
    private float minFadeTime = 1.0f;
    private float maxFadeTime = 4.0f;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        fadeTime = Random.Range(minFadeTime, maxFadeTime);

        StartCoroutine("TwinkleLoop");
    }
    private IEnumerator TwinkleLoop()
    {
        while (true)
        {
            yield return StartCoroutine(FadeEffect(1, (float)0.5));

            yield return StartCoroutine(FadeEffect((float)0.5, 1));
        }
    }

    private IEnumerator FadeEffect(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(start, end, percent);
            spriteRenderer.color = color;

            yield return null;
        }
    }
}
