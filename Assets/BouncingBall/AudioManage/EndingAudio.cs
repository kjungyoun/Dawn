using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingAudio : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject endingmusic;

    private void Awake()
    {
        endingmusic = gameObject;
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(endingmusic);

    }

    private void Update()
    {
        var obj = FindObjectsOfType<AudioPlay>();
        endingmusic = gameObject;
        audioSource = GetComponent<AudioSource>();

        if(SceneManager.GetActiveScene().name == "IntroScene")
        {
            Destroy(endingmusic);
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