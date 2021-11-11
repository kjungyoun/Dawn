using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DinnerGameAudioPlay : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject dinnergame;

    private void Awake()
    {
        dinnergame = gameObject;
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(dinnergame);

    }

    private void Update()
    {
        var obj = FindObjectsOfType<AudioPlay>();
        dinnergame = gameObject;
        audioSource = GetComponent<AudioSource>();

        if(SceneManager.GetActiveScene().name == "GoHomeScene")
        {
            Destroy(dinnergame);
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