using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public Animator Anigame;
    public GameObject guidePanel;
    public string next;

   public void NextScene()
    {
        Anigame.SetBool("isClick", true);
        StartCoroutine(LoadCoroutine(next));
    }

    IEnumerator LoadCoroutine(string next)
    {
        yield return new WaitForSeconds(0.9f);
        SceneLoader.LoadScene(next);
    }

    public void ShowGuide()
    {
        guidePanel.SetActive(true);
    }

    public void CloseGuide()
    {
        guidePanel.SetActive(false);
    }
}
