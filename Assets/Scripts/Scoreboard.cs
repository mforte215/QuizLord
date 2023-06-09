using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{

    [SerializeField] private int totalQuestions = 5;
    [SerializeField] private int currentQuestionTotal = 0;

    [SerializeField] private int NumOfCorrectAnswers = 0;

    public Scoreboard()
    {

    }

    public Scoreboard(int setTotalQuestions)
    {
        totalQuestions = setTotalQuestions;
    }

    public void SetCurrentQuestionTotal(int newQuestionTotal)
    {
        currentQuestionTotal = newQuestionTotal;
    }

    public int GetCurrentQuestionTotal()
    {
        return currentQuestionTotal;
    }

    public int GetCurrentCorrectAnswerTotal()
    {
        return NumOfCorrectAnswers;
    }

    public void AddCorrectAnswer()
    {
        NumOfCorrectAnswers++;
        currentQuestionTotal++;
    }

    public void AddWrongAnswer()
    {
        currentQuestionTotal++;
    }

    public void SetTotalQuestions(int newTotalQuestion)
    {
        totalQuestions = newTotalQuestion;
    }

    public int GetTotalQuestions()
    {
        return totalQuestions;
    }

    public void Reset()
    {
        currentQuestionTotal = 0;
        NumOfCorrectAnswers = 0;
    }


    void Start()
    {

    }
    void Update()
    {

    }



}
