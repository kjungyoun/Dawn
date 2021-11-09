using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SojuGameManager : MonoBehaviour
{
    public string next;
    public void NextScene()
    {
        StartCoroutine(LoadCoroutine(next));
    }

    IEnumerator LoadCoroutine(string next)
    {
        yield return new WaitForSeconds(0.9f);
        SceneManager.LoadScene(next);
    }
}
