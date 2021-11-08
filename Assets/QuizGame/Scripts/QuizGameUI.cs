using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizGameUI : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private QuizManager quizManager;               
    [SerializeField] private CategoryBtnScript categoryBtnPrefab;
    [SerializeField] private GameObject scrollHolder;
    [SerializeField] private Text scoreText, timerText;
    [SerializeField] private List<Image> lifeImageList;
    [SerializeField] private GameObject gameOverPanel, mainMenu, gamePanel;
    [SerializeField] private Color correctCol, wrongCol, normalCol; // 성공 실패 기본
    [SerializeField] private Image questionImg;                     
    [SerializeField] private UnityEngine.Video.VideoPlayer questionVideo;   
    [SerializeField] private AudioSource questionAudio;             
    [SerializeField] private Text questionInfoText;                 
    [SerializeField] private List<Button> options;                  
#pragma warning restore 649

    private float audioLength;          
    private Question question;          // 현재 질문데이터 저장
    private bool answered = false;

    // getter
    public Text TimerText { get => timerText; }                     
    public Text ScoreText { get => scoreText; }
    public GameObject GameOverPanel { get => gameOverPanel; }

    private void Start()
    {
        // 모든 버튼에 listner 추가
        for (int i = 0; i < options.Count; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }

        CreateCategoryButtons();

    }
    
    public void SetQuestion(Question question)
    {
        this.question = question;

        switch (question.questionType)
        {
            case QuestionType.TEXT:
                questionImg.transform.parent.gameObject.SetActive(false);   
                break;
            case QuestionType.IMAGE:
                questionImg.transform.parent.gameObject.SetActive(true);    
                questionVideo.transform.gameObject.SetActive(false);        
                questionImg.transform.gameObject.SetActive(true);           
                questionAudio.transform.gameObject.SetActive(false);        

                questionImg.sprite = question.questionImage;                //set the image sprite
                break;
            case QuestionType.AUDIO:
                questionVideo.transform.parent.gameObject.SetActive(true);  
                questionVideo.transform.gameObject.SetActive(false);        
                questionImg.transform.gameObject.SetActive(false);          
                questionAudio.transform.gameObject.SetActive(true);         
                
                audioLength = question.audioClip.length;                    //set audio clip
                StartCoroutine(PlayAudio());                                //start Coroutine
                break;
            case QuestionType.VIDEO:
                questionVideo.transform.parent.gameObject.SetActive(true);  
                questionVideo.transform.gameObject.SetActive(true);         
                questionImg.transform.gameObject.SetActive(false);          
                questionAudio.transform.gameObject.SetActive(false);        

                questionVideo.clip = question.videoClip;                    //set video clip
                questionVideo.Play();                                       //play video
                break;
        }

        questionInfoText.text = question.questionInfo;                      //set the question text

        // 보기 shuffle
        List<string> ansOptions = ShuffleList.ShuffleListItems<string>(question.options);

        // assign options to respective option buttons
        for (int i = 0; i < options.Count; i++)
        {
            // set the child text
            options[i].GetComponentInChildren<Text>().text = ansOptions[i];
            options[i].name = ansOptions[i];    //set the name of button
            options[i].image.color = normalCol; //set color of button to normal
        }

        answered = false;                       

    }

    public void ReduceLife(int remainingLife)
    {
        lifeImageList[remainingLife].color = Color.red;
    }

    IEnumerator PlayAudio()
    {
        if (question.questionType == QuestionType.AUDIO)
        {
            questionAudio.PlayOneShot(question.audioClip);            
            yield return new WaitForSeconds(audioLength + 0.5f); // 잠시 대기
            StartCoroutine(PlayAudio());
        }
        else
        {
            StopCoroutine(PlayAudio());
            yield return null;
        }
    }

    void OnClick(Button btn)
    {
        if (quizManager.GameStatus == GameStatus.PLAYING)
        {
            if (!answered)
            {
                answered = true;
                bool val = quizManager.Answer(btn.name);

                if (val)
                {
                    StartCoroutine(BlinkImg(btn.image));
                }
                else
                {
                    btn.image.color = wrongCol;
                }
            }
        }
    }

    void CreateCategoryButtons()
    {
        //we loop through all the available catgories in our QuizManager
        for (int i = 0; i < quizManager.QuizData.Count; i++)
        {
            //Create new CategoryBtn
            CategoryBtnScript categoryBtn = Instantiate(categoryBtnPrefab, scrollHolder.transform);
            //Set the button default values
            categoryBtn.SetButton(quizManager.QuizData[i].categoryName, quizManager.QuizData[i].questions.Count);
            int index = i;
            //Add listner to button which calls CategoryBtn method
            categoryBtn.Btn.onClick.AddListener(() => CategoryBtn(index, quizManager.QuizData[index].categoryName));
        }
    }

    //Method called by Category Button
    private void CategoryBtn(int index, string category)
    {
        quizManager.StartGame(index, category); //start the game
        mainMenu.SetActive(false);              //deactivate mainMenu
        gamePanel.SetActive(true);              //activate game panel
    }

    //this give blink effect [if needed use or dont use]
    IEnumerator BlinkImg(Image img)
    {
        for (int i = 0; i < 2; i++)
        {
            img.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            img.color = correctCol;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
