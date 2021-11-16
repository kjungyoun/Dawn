using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayPart : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject musicpart;

    private void Awake()
    {
        musicpart = gameObject;
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(musicpart);

    }

    private void Update()
    {
        var obj = FindObjectsOfType<AudioPlay>();
        musicpart = gameObject;
        audioSource = GetComponent<AudioSource>();

        if(SceneManager.GetActiveScene().name == "CafeScene")
        {
            Destroy(musicpart);
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