using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterviewGameAudio : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject interviewgamemusic;

    private void Awake()
    {
        interviewgamemusic = gameObject;
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(interviewgamemusic);

    }

    private void Update()
    {
        var obj = FindObjectsOfType<AudioPlay>();
        interviewgamemusic = gameObject;
        audioSource = GetComponent<AudioSource>();

        if(SceneManager.GetActiveScene().name == "FinalScene")
        {
            Destroy(interviewgamemusic);
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