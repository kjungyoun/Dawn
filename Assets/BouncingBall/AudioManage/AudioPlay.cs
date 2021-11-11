using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlay : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject musics;

    private void Awake()
    {
        var obj = FindObjectsOfType<AudioPlay>();
        musics = gameObject;
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(musics);

    }

    private void Update()
    {
        var obj = FindObjectsOfType<AudioPlay>();
        musics = gameObject;
        audioSource = GetComponent<AudioSource>();

        if(SceneManager.GetActiveScene().name == "PartTimeScene")
        {
            Destroy(musics);
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