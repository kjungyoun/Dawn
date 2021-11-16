using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DishGameManager : MonoBehaviour
{
    public Animator Anigame;
    public GameObject guidePanel;

    public void NextScene()
    {

        Application.runInBackground = true;

        DirectoryInfo directory = new DirectoryInfo(Application.streamingAssetsPath);
        StageController.maxStageCount = 3;

        Anigame.SetBool("isClick", true);
        StartCoroutine(LoadCoroutine());
    }

    IEnumerator LoadCoroutine()
    {
        yield return new WaitForSeconds(0.9f);
        SceneLoader.LoadScene("Stage");
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
