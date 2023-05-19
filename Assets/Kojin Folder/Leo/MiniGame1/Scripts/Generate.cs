using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generate : MonoBehaviour
{
    public List<QnA> Sentences;
    public int CurrentQuestion;
    public GameObject[] options;
    public Text QuestionTxT;
    public bool isCorrect = false;
    //public Generate QuizManager;
    public Countdown timer;

    public void Start()
    {
        generateQuestion();
    }

    public void correct()
    {
        Sentences.RemoveAt(CurrentQuestion);
        timer.TimeLeft = 10;
        generateQuestion();
    }

    void SetAnswer()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Answer>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = Sentences[CurrentQuestion].Answers[i];
            if(Sentences[CurrentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<Answer>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        if(Sentences.Count > 0)
        {
            CurrentQuestion = Random.Range(0, Sentences.Count);
            QuestionTxT.text = Sentences[CurrentQuestion].Question;
            SetAnswer();
        }
        else
        {
            Debug.Log("Out of Question");
        }
    }

    public void answer()
    {
        if (isCorrect)
        {
            Debug.Log("CorrectAnswer");
            correct();
        }
        else
        {
            Debug.Log("WrongAnswer");
            correct();
        }
    }
}
