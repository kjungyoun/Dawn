using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{

    public GameObject StartPanel;
    public GameObject IntroPanel;
    // Start is called before the first frame update
    void Start()
    {
      StartCoroutine(DelayTime(2));

    }
    IEnumerator DelayTime(float time)
    {
      yield return new WaitForSeconds(time);

      IntroPanel.SetActive(false);
      StartPanel.SetActive(true);
    }

    public void GoGameScene()
    {
        SceneManager.LoadScene("InputNameScene");
    }
}
