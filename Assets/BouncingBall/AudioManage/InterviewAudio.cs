using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterviewAudio : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject interviewmusic;

    private void Awake()
    {
        interviewmusic = gameObject;
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(interviewmusic);

    }

    private void Update()
    {
        var obj = FindObjectsOfType<AudioPlay>();
        interviewmusic = gameObject;
        audioSource = GetComponent<AudioSource>();

        if(SceneManager.GetActiveScene().name == "ShootingGameSceneNoConflict")
        {
            Destroy(interviewmusic);
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