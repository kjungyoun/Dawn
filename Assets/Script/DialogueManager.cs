using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    private float curTime = 0f;
    private float maxTime = 1.0f;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion Singleton

    public Text text;
    public SpriteRenderer rendererSprite; // sprite = audioclip, spriterenderer = audiosource;
    public SpriteRenderer rendererDialogueWindow;

    private List<string> listSentences;
    private List<Sprite> listSprites;
    private List<Sprite> listDialogueWindows;

    private int count; // 대화 진행상황 count

    public Animator aniSprite;
    public Animator aniDialogueWindow;

    public bool talking = false; // 현재 대화중인지?

    private FadeManager fadeManager;
    public Image image; // fade 용 Image
    public string sceneName; // 전환할 scene 이름

    private int flag = 0;
    // Getter
    public int Count
    {
        get
        {
            return count;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        text.text = "";
        count = 0;
        listSentences = new List<string>();
        listSprites = new List<Sprite>();
        listDialogueWindows = new List<Sprite>();

    }

    public void ShowDialogue(Dialogue dialogue)
    {

        for(int i=0; i<dialogue.sentences.Length; i++)
        {
            // ListSenctences에 dialogue에 있는 모든 sentence 추가
            listSentences.Add(dialogue.sentences[i]);
            // Person도 list에 추가
            listSprites.Add(dialogue.persons[i]);
            // window list에 추가
            listDialogueWindows.Add(dialogue.dialogueWindows[i]);
        }
        talking = true;

        aniSprite.SetBool("isAppear", true);
        aniDialogueWindow.SetBool("isAppear", true);
        StartCoroutine(StartDialogueCoroutine());
    }

    public void ExitDialogue()
    {
        count = 0;
        text.text = "";
        listSentences.Clear();
        listSprites.Clear();
        listDialogueWindows.Clear();
        aniSprite.SetBool("isAppear", false);
        aniDialogueWindow.SetBool("isAppear", false);
        talking = false;
        fadeManager = FindObjectOfType<FadeManager>();
        fadeManager.FadeIn(image);
        StartCoroutine(LoadCoroutine(sceneName));
    }

    IEnumerator StartDialogueCoroutine()
    {
        talking = true;
        if (count > 0)
        {
            // 대화창 교체 (사람이 달라질 때)
            if (listDialogueWindows[count] != listDialogueWindows[count - 1])
            {
                aniSprite.SetBool("isChange", true);
                aniDialogueWindow.SetBool("isAppear", false);
                yield return new WaitForSeconds(0.2f);
                rendererDialogueWindow.GetComponent<SpriteRenderer>().sprite = listDialogueWindows[count];
                rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprites[count];
                aniDialogueWindow.SetBool("isAppear", true);
                aniSprite.SetBool("isChange", false);
            }
            else
            { // 사람은 그대로지만 사람 이미지를 다르게 쓸 때 (표정 변화)
                // 스프라이트 출력 
                if (listSprites[count] != listSprites[count - 1]) // 이전 스프라이트랑 다음 스프라이트랑 다르면 교체
                {
                    aniSprite.SetBool("isChange", true);
                    yield return new WaitForSeconds(0.1f);
                    rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprites[count];
                    aniSprite.SetBool("isChange", false);
                }
                else
                { // 대화창도 똑같고 사람 이미지도 똑같을 때
                    yield return new WaitForSeconds(0.05f);
                }
            }
        }
        else
        {
            // 첫 이미지일 때
            rendererDialogueWindow.GetComponent<SpriteRenderer>().sprite = listDialogueWindows[count];
            rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprites[count];
        }

        yield return new WaitForSeconds(0.3f);

        // 텍스트 출력
        for (int i = 0; i<listSentences[count].Length; i++)
        {
            if(listSentences[count][i].Equals('뜐')) // 주인공 이름일 때
            {
                for(int j=0; j<Dialogue.yourName.Length; j++)
                {
                    text.text += Dialogue.yourName[j];
                    yield return new WaitForSeconds(0.01f);
                }  
            }
            else if (listSentences[count][i].Equals('뀽')) // 상대방 이름일 때
            {
                for (int j = 0; j < Dialogue.herName.Length; j++)
                {
                    text.text += Dialogue.herName[j];
                    yield return new WaitForSeconds(0.01f);
                }
            }
            else // 이름이 아닐 때
            {
                text.text += listSentences[count][i]; // 1번째 문장, 가나다라마바사 한 글자씩 출력
            }
            yield return new WaitForSeconds(0.01f); // 출력 사이에 0.01초 딜레이 줌
        }
        talking = false;
    }

    void curTimeUp()
    {
        curTime += Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        curTimeUp();
        if (curTime < maxTime)
            return;

        if (!talking && curTime >= maxTime)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Ended)
                {
                    count++;
                    text.text = "";

                    if (count >= listSentences.Count)
                    {
                        flag++;
                    }
                    if (flag == 1 && count >= listSentences.Count) // 마지막 대화일 때
                    {
                        StopAllCoroutines();
                        ExitDialogue(); // 대화 종료
                    }
                    else if (flag > 1)
                    {
                        return;
                    }
                    else
                    {
                        StopAllCoroutines();
                        StartCoroutine(StartDialogueCoroutine());
                    }
                }
            }
        }
#if UNITY_EDITOR
        if (!talking && curTime >= maxTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                count++;
                text.text = "";

                if (count >= listSentences.Count)
                {
                    flag++;
                }
                if (flag == 1 && count >= listSentences.Count) // 마지막 대화일 때
                {
                    StopAllCoroutines();
                    ExitDialogue(); // 대화 종료
                }
                else if (flag > 1)
                {
                    return;
                }
                else
                {
                    StopAllCoroutines();
                    StartCoroutine(StartDialogueCoroutine());
                }
            }
        }
#endif
    }

    IEnumerator LoadCoroutine(string sceneName)
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
     }
}
