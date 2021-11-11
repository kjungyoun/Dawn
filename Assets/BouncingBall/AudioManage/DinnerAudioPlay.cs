using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DinnerAudioPlay : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject musicdinner;

    private void Awake()
    {
        musicdinner = gameObject;
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(musicdinner);

    }

    private void Update()
    {
        var obj = FindObjectsOfType<AudioPlay>();
        musicdinner = gameObject;
        audioSource = GetComponent<AudioSource>();

        if(SceneManager.GetActiveScene().name == "SojuGamePlayScene")
        {
            Destroy(musicdinner);
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