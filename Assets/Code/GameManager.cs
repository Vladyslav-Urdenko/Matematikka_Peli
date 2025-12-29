using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;  
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Question[] questions;
    private static List<Question> unansweredQuestions;

    private Question currentQuestion;

    [SerializeField]
    private TextMeshProUGUI questionTextG;

    [SerializeField]
    private TextMeshProUGUI trueAnswerText;

    [SerializeField]
    private TextMeshProUGUI falseAnswerText;

    [SerializeField]
    private Animator animator;

    [SerializeField]

    private float timeBetweenQuestions = 1f;

    [Header("Progress")]
    [SerializeField] private int questionsPerLevel = 3;
    private static int answeredCount = 0;

    [Header("UI")]
    [SerializeField] private GameObject continueMenu;

    [SerializeField] private GameObject hideMenu;

    private static int correctAnswers = 0;
    void Start()
    {
        if(unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }

        SetCurrentQuestion();
        
    }

    void SetCurrentQuestion()
    {
        int randomIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomIndex];

        questionTextG.text = currentQuestion.questionText;

        if (currentQuestion.isTrue)
        {
            trueAnswerText.text = "? Onnea!";
            falseAnswerText.text = "? Hylätty!";
        }
        else
        {
            trueAnswerText.text = "? Hylätty!";
            falseAnswerText.text = "?? Onnea!";
        }

        //unansweredQuestions.RemoveAt(randomIndex);
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);
        answeredCount++;
        yield return new WaitForSeconds(timeBetweenQuestions);
        if (answeredCount >= questionsPerLevel)
        {
            continueMenu.SetActive(true);
            hideMenu.SetActive(false);
            //int levelIndex = SceneManager.GetActiveScene().buildIndex;
            //PlayerPrefs.SetInt("Level_Stars_" + levelIndex, correctAnswers);
            SaveStars();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void UserSelectTrue()
    {
        animator.SetTrigger("True");
        if (currentQuestion.isTrue)
        {
            Debug.Log("Correct");
            correctAnswers++;
        }
        else
        {
            Debug.Log("Wrong");
        }
        StartCoroutine(TransitionToNextQuestion());
    }   

    public void UserSelectFalse()
    {
        animator.SetTrigger("False");
        if (!currentQuestion.isTrue)
        {
            Debug.Log("Correct");
            correctAnswers++;
        }
        else
        {
            Debug.Log("Wrong");
        }
        StartCoroutine(TransitionToNextQuestion());
    }

    public void LoadNextLevel()
    {
        answeredCount = 0;
        correctAnswers = 0;
        unansweredQuestions = null;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackToLevelSelect()
    {
        answeredCount = 0;
        correctAnswers = 0;
        unansweredQuestions = null;

        SceneManager.LoadScene(1);
    }

    void SaveStars()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        string key = "Level_Stars_" + levelIndex;

        int previousStars = PlayerPrefs.GetInt(key, 0);

        if (correctAnswers > previousStars)
        {
            PlayerPrefs.SetInt(key, correctAnswers);
            PlayerPrefs.Save();
        }
    }


}
