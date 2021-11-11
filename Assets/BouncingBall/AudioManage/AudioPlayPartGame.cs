using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayPartGame : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject musicpartgame;

    private void Awake()
    {
        musicpartgame = gameObject;
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(musicpartgame);

    }

    private void Update()
    {
        var obj = FindObjectsOfType<AudioPlay>();
        musicpartgame = gameObject;
        audioSource = GetComponent<AudioSource>();

        if(SceneManager.GetActiveScene().name == "CafeScene")
        {
            Destroy(musicpartgame);
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