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
        yield return new WaitForSeconds(timeBetweenQuestions);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UserSelectTrue()
    {
        animator.SetTrigger("True");
        if (currentQuestion.isTrue)
        {
            Debug.Log("Correct");
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
        }
        else
        {
            Debug.Log("Wrong");
        }
        StartCoroutine(TransitionToNextQuestion());
    }
}
