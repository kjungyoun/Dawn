using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlertManager : MonoBehaviour
{
    public Animator headPhoneAni;
    public Animator guideAni;

    // Start is called before the first frame update
    void Start()
    {
        headPhoneAni.SetBool("isAppear", true);
        guideAni.SetBool("isAppear", true);

        StartCoroutine(StartFadeInOut());
    }

    IEnumerator StartFadeInOut()
    {
        yield return new WaitForSeconds(3);

        headPhoneAni.SetBool("isAppear", false);
        guideAni.SetBool("isAppear", false);

        yield return new WaitForSeconds(0.8f);

        SceneManager.LoadScene("BeforeWindowScene");
    }
}
