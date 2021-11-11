using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PjtAudioPlay : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject project;

    private void Awake()
    {
        project = gameObject;
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(project);

    }

    private void Update()
    {
        var obj = FindObjectsOfType<AudioPlay>();
        project = gameObject;
        audioSource = GetComponent<AudioSource>();

        if(SceneManager.GetActiveScene().name == "DinnerScene")
        {
            Destroy(project);
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