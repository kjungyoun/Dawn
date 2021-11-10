using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditManager : MonoBehaviour
{
    private FadeManager fadeManager;

    public Image image;
    public GameObject credit;

    // Start is called before the first frame update
    void Start()
    {
        fadeManager = FindObjectOfType<FadeManager>(); // Fade 관리 Manager 객체 생성

        fadeManager.FadeOut(image); // FadeOut 호출
        StartCoroutine(StartEndingCredit());
    }

    IEnumerator StartEndingCredit()
    {
        yield return new WaitForSeconds(0.5f);

        credit.SetActive(true);

        yield return new WaitForSeconds(46);

        fadeManager.FadeIn(image);

        yield return new WaitForSeconds(1.5f);

        SceneLoader.LoadScene("IntroScene");
    }
}
