using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    // Story 대사 문장들
    [TextArea(1,2)]
    public string[] sentences;
    // 대화하는 사람
    public Sprite[] persons;
    // 대화하는 사람의 대화창
    public Sprite[] dialogueWindows;
}
