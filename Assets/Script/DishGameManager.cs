using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DishGameManager : MonoBehaviour
{
    public Animator Anigame;
    public void NextScene()
    {

        Application.runInBackground = true;
        PlayerPrefs.SetInt("StageIndex", 0);
        DirectoryInfo directory = new DirectoryInfo(Application.streamingAssetsPath);
        StageController.maxStageCount = directory.GetFiles().Length / 2;


        Anigame.SetBool("isClick", true);
        StartCoroutine(LoadCoroutine());
    }

    IEnumerator LoadCoroutine()
    {
        yield return new WaitForSeconds(0.9f);
        SceneLoader.LoadScene("Stage");
    }
}
