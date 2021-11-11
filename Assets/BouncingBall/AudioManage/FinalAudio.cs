using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalAudio : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject finalmusic;

    private void Awake()
    {
        finalmusic = gameObject;
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(finalmusic);

    }

    private void Update()
    {
        var obj = FindObjectsOfType<AudioPlay>();
        finalmusic = gameObject;
        audioSource = GetComponent<AudioSource>();

        if(SceneManager.GetActiveScene().name == "CreditScene")
        {
            Destroy(finalmusic);
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