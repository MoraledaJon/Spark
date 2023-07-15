using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Generate : MonoBehaviour
{
    public List<QnA> Sentences;
    public int CurrentQuestion;
    public int NumberQ;
    public GameObject[] options;
    public GameObject[] NbQuestions;
    public Text QuestionTxT;
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
            SceneManager.LoadScene(15);
        }
    }
}
