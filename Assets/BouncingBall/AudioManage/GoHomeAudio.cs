using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoHomeAudio : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject gohomemusic;

    private void Awake()
    {
        gohomemusic = gameObject;
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gohomemusic);

    }

    private void Update()
    {
        var obj = FindObjectsOfType<AudioPlay>();
        gohomemusic = gameObject;
        audioSource = GetComponent<AudioSource>();

        if(SceneManager.GetActiveScene().name == "BeforeInterviewScene")
        {
            Destroy(gohomemusic);
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