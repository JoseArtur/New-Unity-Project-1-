using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public Question[] questions;
    private static List<Question> unq;

    private Question currentQuestion;

    [SerializeField]
    private Text factText;
    [SerializeField]
    private Text AnswerText;
    [SerializeField]
    Animator animator;

    [SerializeField]
    private float timeBetweenQuestions = 1f;

    void Start ()
    {
        if (unq == null || unq.Count == 0) 
        {
            unq = questions.ToList<Question>();
        }
        SetCurrentQuestion();
    }

    void SetCurrentQuestion ()
    {
        int randomQuestionINdex = Random.Range(0, unq.Count);
        currentQuestion = unq[randomQuestionINdex];
        factText.text = currentQuestion.fact;


        if (currentQuestion.isTrue)
        {
            AnswerText.text = "YES";
        }
        else
        {
            AnswerText.text = "NO";
        }

        unq.RemoveAt(randomQuestionINdex);
    }
    IEnumerator TransitionToNextQuestion()
    {
        unq.Remove(currentQuestion);
        yield return new WaitForSeconds(timeBetweenQuestions);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UserSelect ()
    {
        animator.SetTrigger("Answer");
        if (currentQuestion.isTrue)
        {
            Debug.Log("YES");
        }else 
        {
            Debug.Log("NO");       
        }

        StartCoroutine(TransitionToNextQuestion());
    }
}
