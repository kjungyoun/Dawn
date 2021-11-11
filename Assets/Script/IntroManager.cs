using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public Image image;
    public GameObject obj;
    private FadeManager fadeManager;

    // Start is called before the first frame update
    void Start()
    {
        fadeManager = FindObjectOfType<FadeManager>();
        fadeManager.FadeOut(image);
        StartCoroutine(HidePanel());
    }

    IEnumerator HidePanel()
    {
        yield return new WaitForSeconds(1.5f);
        obj.SetActive(false);
    }


    IEnumerator StartLoadScene()
    {
        obj.SetActive(true);
        fadeManager.FadeIn(image);

        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
        SceneManager.LoadScene("InputNameScene");
    }

    public void GoGameScene()
    {
        StartCoroutine(StartLoadScene());
    }
}
