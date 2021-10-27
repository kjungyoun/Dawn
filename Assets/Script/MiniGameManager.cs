using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    public Animator Anigame;
    public string next;
   public void NextScene()
    {
        Anigame.SetBool("isClick", true);
        StartCoroutine(LoadCoroutine(next));
    }

    IEnumerator LoadCoroutine(string next)
    {
        yield return new WaitForSeconds(0.9f);
        SceneManager.LoadScene(next);
    }
}
