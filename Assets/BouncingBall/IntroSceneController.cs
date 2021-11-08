using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class IntroSceneController : MonoBehaviour
{
    private void Awake()
    {
        Application.runInBackground = true;

        PlayerPrefs.SetInt("StageIndex", 0);

        DirectoryInfo directory = new DirectoryInfo(Application.streamingAssetsPath);

        StageController.maxStageCount = directory.GetFiles().Length / 2;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneLoader.LoadScene("Stage");
        }
    }
}
