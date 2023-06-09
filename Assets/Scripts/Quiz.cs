using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{

    [SerializeField] QuestionScriptableObject[] questions;
    private QuestionScriptableObject currentQuestion;
    [SerializeField] int currentQuestionIndex = 0;

    [SerializeField] int howManyTimesWasTheButtonClicked = 0;

    bool hasAnsweredEarly = false;

    [SerializeField] GameObject quizObject;
    [SerializeField] GameObject resultsObject;
    [SerializeField] float calculatedPercentage;

    public float totalQuestions;
    public float currentTotalQuestions = 0f;
    public float currentCorrectAnswers = 0f;


    [SerializeField] GameObject[] AnswerButtonGroup;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] Sprite normalButtonSprite;
    [SerializeField] Sprite wrongButtonSprite;
    [SerializeField] Sprite rightButtonSprite;
    void Start()
    {

        DisplayQuestion();
    }


    void DisplayQuestion()
    {
        if (currentQuestionIndex < questions.Length)
        {
            currentQuestion = questions[currentQuestionIndex];
            Debug.Log("Loading Question " + currentQuestionIndex);
            questionText.text = currentQuestion.GetQuestion();
            for (int i = 0; i < AnswerButtonGroup.Length; i++)
            {
                TextMeshProUGUI buttonText = AnswerButtonGroup[i].GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = currentQuestion.GetOptionByIndex(i);
            }
        }
        else
        {
            Debug.Log("Ended Quiz!");
            quizObject.SetActive(false);
            TextMeshProUGUI resultsText = resultsObject.GetComponentInChildren<TextMeshProUGUI>();
            calculatedPercentage = (currentCorrectAnswers / currentTotalQuestions) * 100;
            resultsText.text = "RESULTS: " + calculatedPercentage + "%";
            resultsObject.SetActive(true);
            Debug.Log("RESULTS OBJECT LOG:");
            Debug.Log(resultsObject.ToString());
        }

    }


    void LoadNextQuestion()
    {
        //Resetting new question
        Debug.Log("Current Question Index: " + currentQuestionIndex);
        if (currentQuestionIndex < questions.Length)
        {
            currentQuestionIndex++;
            DisplayQuestion();
            ResetButtonSprites();
        }
        else
        {
            quizObject.SetActive(false);

        }


    }

    void ResetButtonSprites()
    {
        for (int i = 0; i < AnswerButtonGroup.Length; i++)
        {
            Image button = AnswerButtonGroup[i].GetComponent<Image>();
            button.sprite = normalButtonSprite;
        }
    }

    public void onButtonClick(int index)
    {

        int currentCorrectAnswerIndex = currentQuestion.GetCorrectAnswer();
        if (index == currentCorrectAnswerIndex)
        {
            DisplayCorrectAnswer(index);
        }
        else
        {
            DisplayWrongAnswer();
        }
    }


    void DisplayCorrectAnswer(int index)
    {
        int currentCorrectAnswerIndex = currentQuestion.GetCorrectAnswer();
        if (index == currentCorrectAnswerIndex)
        {
            questionText.text = "Correct!";
            Image button = AnswerButtonGroup[index].GetComponent<Image>();
            button.sprite = rightButtonSprite;
            currentCorrectAnswers++;
            currentTotalQuestions++;
            calculatedPercentage = (currentCorrectAnswers / currentTotalQuestions) * 100;
            scoreText.text = "Score: " + calculatedPercentage + "%";
            Invoke("LoadNextQuestion", 3f);
        }
    }

    void DisplayWrongAnswer()
    {
        questionText.text = "Wrong!";
        int currentCorrectAnswerIndex = currentQuestion.GetCorrectAnswer();
        for (int i = 0; i < AnswerButtonGroup.Length; i++)
        {
            if (i != currentCorrectAnswerIndex)
            {
                //set the wrong sprite on the wrong buttons
                Debug.Log("Changing Sprite to Wrong Color!");
                Image button = AnswerButtonGroup[i].GetComponent<Image>();
                button.sprite = wrongButtonSprite;
            }
            else
            {
                Debug.Log("Changien Sprite to Correct Color!");
                Image button = AnswerButtonGroup[i].GetComponent<Image>();
                button.sprite = rightButtonSprite;
            }
        }
        currentTotalQuestions++;
        calculatedPercentage = (currentCorrectAnswers / currentTotalQuestions) * 100;
        scoreText.text = "Score: " + Mathf.Round(calculatedPercentage) + "%";
        Invoke("LoadNextQuestion", 3f);
    }













    /* 
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionScriptableObject> questions = new List<QuestionScriptableObject>();
    QuestionScriptableObject question;
    [SerializeField] GameObject[] answerButtons;
    int correcAnswerIndex;
    int currentQuestionIndex = 0;
    int currentScoreTotal = 0;

    Timer timer;
    [SerializeField] Image timerImage;

    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite wrongAnswerSprite;

    [SerializeField] TextMeshProUGUI scoreText;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
        GetNextQuestion();
    }

    void Update()
    {
        if (!timer.outOfTime)
        {
            if (timer.isAnsweringQuestion)
            {
                timerImage.fillAmount = timer.fillFraction;
            }
        }
        else
        {
            setWrongAnswer();
            Invoke("GetNextQuestion", 3f);
        }


    }

    void GetNextQuestion()
    {
        ResetButtonSprites();
        timer.ResetTimer();
        SetAllButtons(true);
        setCurrentQuestion();
        DisplayQuestion();

    }

    void setCurrentQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        {
            question = questions[currentQuestionIndex];
            currentQuestionIndex++;
        }

    }

    void setCurrentScore(bool rightAnswer)
    {
        if (rightAnswer)
        {
            currentScoreTotal++;
            scoreText.text = "Score: " + currentScoreTotal + " / " + questions.Count;
        }
        else
        {
            scoreText.text = "Score: " + currentScoreTotal + " / " + questions.Count;
        }
    }


    private void DisplayQuestion()
    {
        questionText.text = question.GetQuestion();
        for (int i = 0; i < 4; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }
    }

    private void ResetButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonSprite = answerButtons[i].GetComponent<Image>();
            buttonSprite.sprite = defaultAnswerSprite;
        }
    }

    private void SetAllButtons(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }


    public void OnAnswerSelected(int index)
    {

        if (index == question.GetCorrectAnswer())
        {
            questionText.text = "Correct!";
            Image buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            setCurrentScore(true);
            Invoke("GetNextQuestion", 3f);
        }

        else
        {
            setWrongAnswer();
            setCurrentScore(false);
            Invoke("GetNextQuestion", 3f);

        }
    }


    public void setWrongAnswer()
    {
        questionText.text = "Wrong!";
        correcAnswerIndex = question.GetCorrectAnswer();
        for (int i = 0; i < 4; i++)
        {
            if (i == correcAnswerIndex)
            {
                Debug.Log("Setting the Correct sprite!");
                Image buttonImage = answerButtons[i].GetComponent<Image>();
                buttonImage.sprite = correctAnswerSprite;
            }
            else
            {
                Debug.Log("Setting wrong sprite! ;)");
                Image buttonImage = answerButtons[i].GetComponent<Image>();
                buttonImage.sprite = wrongAnswerSprite;
            }
        }
        SetAllButtons(false);
    }
 */
}
