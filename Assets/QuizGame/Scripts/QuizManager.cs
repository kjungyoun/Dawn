using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private QuizGameUI quizGameUI;
    [SerializeField] private List<QuizDataScriptable> quizDataList;
    [SerializeField] private float timeInSeconds;
#pragma warning restore 649

    private string currentCategory = "";
    //private int correctAnswerCount = 0;
    private List<Question> questions;
    private Question selectedQuetion = new Question();
    private int gameScore;
    private int lifesRemaining;
    private float currentTime;
    private QuizDataScriptable dataScriptable;

    private GameStatus gameStatus = GameStatus.NEXT;

    public GameStatus GameStatus { get { return gameStatus; } }

    public List<QuizDataScriptable> QuizData { get => quizDataList; }

    public void StartGame(int categoryIndex, string category)
    {
        currentCategory = category;
        //correctAnswerCount = 0;
        gameScore = 0;
        lifesRemaining = 3;
        currentTime = timeInSeconds;
        questions = new List<Question>(); // set the questions data
        dataScriptable = quizDataList[categoryIndex];
        questions.AddRange(dataScriptable.questions);
        SelectQuestion();
        gameStatus = GameStatus.PLAYING;
    }

    /// <summary>
    /// Method used to randomly select the question form questions data
    /// </summary>
    private void SelectQuestion()
    {
        int val = UnityEngine.Random.Range(0, questions.Count);
        selectedQuetion = questions[val];
        quizGameUI.SetQuestion(selectedQuetion); // quizGameUI로 question 보내기

        questions.RemoveAt(val);
    }

    private void Update()
    {
        if (gameStatus == GameStatus.PLAYING)
        {
            currentTime -= Time.deltaTime;
            SetTime(currentTime);
        }
    }

    void SetTime(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        quizGameUI.TimerText.text = time.ToString("mm':'ss");   // 분:초 출력

        if (currentTime <= 0)
        {
            // GameOver
            GameEnd();
        }
    }

    /// <summary>
    /// Method called to check the answer is correct or not
    /// </summary>
    /// <param name="selectedOption">answer string</param>
    /// <returns></returns>
    public bool Answer(string selectedOption)
    {
        bool correct = false;
        
        if (selectedQuetion.correctAns == selectedOption)
        {
            // 정답 O
            //correctAnswerCount++;
            correct = true;
            gameScore += 20;
            quizGameUI.ScoreText.text = "Score:" + gameScore;
        }
        else
        {
            // 정답 X
            lifesRemaining--;
            quizGameUI.ReduceLife(lifesRemaining);

            if (lifesRemaining == 0)
            {
                // 게임 실패시
                GameOver();
            }
        }

        if (gameStatus == GameStatus.PLAYING)
        {
            if (questions.Count > 0)
            {
                // SelectQuestion 1초 후 다시 불러오기
                Invoke("SelectQuestion", 0.4f);
            }
            else // 전부다 성공했을 때
            {
                GameEnd();
            }
        }
        return correct;
    }


    private void GameEnd() // 게임 성공시
    {
        gameStatus = GameStatus.NEXT;
        quizGameUI.GameEndPanel.SetActive(true);

        //fi you want to save only the highest score then compare the current score with saved score and if more save the new score
        //eg:- if correctAnswerCount > PlayerPrefs.GetInt(currentCategory) then call below line

        // 최근 score 저장
        //PlayerPrefs.SetInt(currentCategory, correctAnswerCount); //save the score for this category
    }

    private void GameOver() // 게임 실패시
    {
        gameStatus = GameStatus.NEXT;
        quizGameUI.GameOverPanel.SetActive(true);

        //fi you want to save only the highest score then compare the current score with saved score and if more save the new score
        //eg:- if correctAnswerCount > PlayerPrefs.GetInt(currentCategory) then call below line

        // 최근 score 저장
        //PlayerPrefs.SetInt(currentCategory, correctAnswerCount); //save the score for this category
    }
}

[System.Serializable]
public class Question
{
    public string questionInfo;         //question text
    public QuestionType questionType;   //type
    public Sprite questionImage;        //image for Image Type
    public AudioClip audioClip;         //audio for audio type
    public UnityEngine.Video.VideoClip videoClip;   //video for video type
    public List<string> options;        //options to select
    public string correctAns;           //correct option
}

[System.Serializable]
public enum QuestionType
{
    TEXT,
    IMAGE,
    AUDIO,
    VIDEO
}

[SerializeField]
public enum GameStatus
{
    PLAYING,
    NEXT
}