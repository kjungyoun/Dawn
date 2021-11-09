using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{

    public static FadeManager instance;

    public void FadeIn(Image image)
    {
        StartCoroutine(FadeInCoroutine(image));
    }

    public void FadeOut(Image image)
    {
        StartCoroutine(FadeOutCoroutine(image));
    }

    IEnumerator FadeInCoroutine(Image image)
    {
        float fadeCount = 0; // 처음 알파값

        while(fadeCount < 1.0f) // 알파 최대값 1.0까지 반복
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f); // 0.01초마다 실행
            image.color = new Color(0, 0, 0, fadeCount); // 해당 변수값으로 알파값 지정
        }
    }

    IEnumerator FadeOutCoroutine(Image image)
    {
        float fadeCount = 1.0f;
        while (fadeCount > 0) // 알파 최소값 0까지 반복
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f); // 0.01초마다 실행
            image.color = new Color(0, 0, 0, fadeCount); // 해당 변수값으로 알파값 지정
        }
    }
}
