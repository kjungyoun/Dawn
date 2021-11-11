using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    public Animator Anigame;
    public string next;
   public void NextScene()
    {

        Application.runInBackground = true;
        PlayerPrefs.SetInt("StageIndex", 0);
        DirectoryInfo directory = new DirectoryInfo(Application.streamingAssetsPath);
        StageController.maxStageCount = directory.GetFiles().Length / 2;


        Anigame.SetBool("isClick", true);
        StartCoroutine(LoadCoroutine(next));
    }

    IEnumerator LoadCoroutine(string next)
    {
        yield return new WaitForSeconds(0.9f);
        SceneLoader.LoadScene("Stage");
    }
}
