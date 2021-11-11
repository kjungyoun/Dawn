using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputNameManager : MonoBehaviour
{

    public Image image;
    public Text yourName;
    public Text herName;
    public string next;
    private FadeManager fadeManager;
    public GameObject gameObject;

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
        gameObject.SetActive(false);
    }

    public void onClick()
    {
        // 이름 저장
        Dialogue.yourName = yourName.text;
        Dialogue.herName = herName.text;

        Debug.Log(Dialogue.yourName);
        Debug.Log(Dialogue.herName);


        StartCoroutine(StartLoadScene());
    }

    IEnumerator StartLoadScene()
    {
        gameObject.SetActive(true);
        fadeManager.FadeIn(image);

        yield return new WaitForSeconds(1.5f);

        SceneLoader.LoadScene(next);
    }

}
