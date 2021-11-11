using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayCafe : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject musiccafe;

    private void Awake()
    {
        musiccafe = gameObject;
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(musiccafe);

    }

    private void Update()
    {
        var obj = FindObjectsOfType<AudioPlay>();
        musiccafe = gameObject;
        audioSource = GetComponent<AudioSource>();

        if(SceneManager.GetActiveScene().name == "QuizGameScene")
        {
            Destroy(musiccafe);
        }
        //if (obj.Length != 1)
        //{
        //    Debug.Log("ss");
        //    Destroy(musics);
        //}
        //else
        //{
        //    Debug.Log("dd");
        //}
       
    }
}