using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class MiniGameManager : MonoBehaviour
{
    [Header("言葉の順番を選ぶ")]
    public List<string> words;

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI timerText;

    [Header("タイマー時間")]
    public float timeRemaining = 10f;

    public bool timerIsRunning = false;

    private char[] letters;

    public List<Button> buttons;

    public string word;

    public ButtonManager buttonManager;



    // Start is called before the first frame update
    void Start()
    {
        Shuffle("");
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                buttonManager.WinOrLose(false); 
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
        
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Shuffle(string usedWord)
    {
        Debug.Log("shuffling " + usedWord);
        for (int i = 0; i < words.Count; i++)
        {
            if (words[i] == usedWord)
            {
                words.RemoveAt(i);
            }
        }

        int rnd = Random.Range(0, words.Count);
        
        word = words[rnd];

        questionText.text = "Q. " + word + " になる様に選んで";

        timerIsRunning = true;

        letters = word.ToCharArray();

        for (int i = letters.Length - 1; i > 0; i--)
        {
            var j = Random.Range(0, i + 1); // ランダムで要素番号を１つ選ぶ（ランダム要素）
            var temp = letters[i]; // 一番最後の要素を仮確保（temp）にいれる
            letters[i] = letters[j]; // ランダム要素を一番最後にいれる
            letters[j] = temp; // 仮確保を元ランダム要素に上書き
        }

        for (int i = 0; i < letters.Length; i++)
        {
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = letters[i] + "";
        }
    }

}
