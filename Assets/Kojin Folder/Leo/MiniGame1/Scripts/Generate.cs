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

    public void Start()
    {
        generateQuestion();
    }

    public void correct()
    {
        Sentences.RemoveAt(CurrentQuestion);
        generateQuestion();
    }

    void SetAnswer()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Answer>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = Sentences[CurrentQuestion].Answer[i];
            if(Sentences[CurrentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<Answer>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        CurrentQuestion = Random.Range(0, Sentences.Count);
        QuestionTxT.text = Sentences[CurrentQuestion].Question;
    }
}
