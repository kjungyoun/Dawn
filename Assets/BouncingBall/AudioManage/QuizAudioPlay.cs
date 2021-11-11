using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizAudioPlay : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject quizmusic;

    private void Awake()
    {
        quizmusic = gameObject;
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(quizmusic);

    }

    private void Update()
    {
        var obj = FindObjectsOfType<AudioPlay>();
        quizmusic = gameObject;
        audioSource = GetComponent<AudioSource>();

        if(SceneManager.GetActiveScene().name == "DoPjtScene")
        {
            Destroy(quizmusic);
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