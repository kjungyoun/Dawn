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

    public Animator[] ani;

    // Start is called before the first frame update
    void Start()
    {
        fadeManager = FindObjectOfType<FadeManager>();
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
        fadeManager.FadeIn(image);

        for(int i=0; i<ani.Length; i++)
        {
            ani[i].SetBool("isAppear", false);
        }

        yield return new WaitForSeconds(1.5f);

        SceneLoader.LoadScene(next);
    }

}
