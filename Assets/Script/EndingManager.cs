using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public Animator end1;
    public Animator end2;

    // Start is called before the first frame update
    void Start()
    {
        end1.SetBool("isAppear", true);

        StartCoroutine(StartEnd());

    }

    IEnumerator StartEnd()
    {
        yield return new WaitForSeconds(2);

        end1.SetBool("isAppear", false);

        yield return new WaitForSeconds(0.7f);

        end2.SetBool("isAppear", true);

        yield return new WaitForSeconds(2);

        end2.SetBool("isAppear", false);

        yield return new WaitForSeconds(0.7f);

        SceneLoader.LoadScene("IntroScene");

    }
}
