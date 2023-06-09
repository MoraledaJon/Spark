using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour
{
    public bool isCorrect = false;
    public Generate QuizManager;
    public Image Maru;
    public Image Batsu;

    void Start()
    {
        Maru.gameObject.SetActive(false);
        Batsu.gameObject.SetActive(false);

    }

    public void answer()
    {
        if(isCorrect)
        {
            Maru.gameObject.SetActive(true);
            Batsu.gameObject.SetActive(false);
            Debug.Log("CorrectAnswer");
            QuizManager.correct();
        }
        else
        {
            Maru.gameObject.SetActive(false);
            Batsu.gameObject.SetActive(true);
            Debug.Log("WrongAnswer");
            QuizManager.correct();
        }
    }
}
