using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionScriptableObject : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] private string text = "Enter a new question text here";
    [SerializeField] private string[] answers = new string[4];
    [SerializeField] private int correctAnswerIndex;


    public void setQuestion(string questionText)
    {
        text = questionText;
    }

    public string GetQuestion()
    {
        return text;
    }

    public void setCorrectAnswer(int answerIndex)
    {
        correctAnswerIndex = answerIndex;
    }
    public int GetCorrectAnswer()
    {
        return correctAnswerIndex;
    }

    public string GetOptionByIndex(int index)
    {
        return answers[index];
    }

    public string[] GetAnswerArray()
    {
        return answers;
    }


}
