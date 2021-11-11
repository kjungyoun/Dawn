using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{

    Text text;
    public float BlinkFadeInTime = 0.5f;
    public float BlinkStayTime = 0.8f;
    public float BlinkFadeOutTime = 0.7f;
    private Color _color;
    private float time;

    void Start()
    {
        text = GetComponent<Text>();
        _color = text.color;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time < BlinkFadeInTime)
        {
            text.color = new Color(_color.r, _color.g, _color.b, time / BlinkFadeInTime);
        }else if(time < BlinkFadeInTime + BlinkStayTime)
        {
            text.color = new Color(_color.r, _color.g, _color.b, 1);
        }else if(time < BlinkFadeInTime + BlinkStayTime + BlinkFadeOutTime)
        {
            text.color = new Color(_color.r, _color.g, _color.b, 1 - (time - (BlinkFadeInTime+BlinkStayTime))/BlinkFadeOutTime);
        }
        else
        {
            time = 0;
        }
    }
}
