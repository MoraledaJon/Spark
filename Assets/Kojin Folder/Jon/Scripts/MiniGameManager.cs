using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class MiniGameManager : MonoBehaviour
{
    [Header("���t�̏��Ԃ�I��")]
    public List<string> words;

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI timerText;

    [Header("�^�C�}�[����")]
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

        questionText.text = "Q. " + word + " �ɂȂ�l�ɑI���";

        timerIsRunning = true;

        letters = word.ToCharArray();

        for (int i = letters.Length - 1; i > 0; i--)
        {
            var j = Random.Range(0, i + 1); // �����_���ŗv�f�ԍ����P�I�ԁi�����_���v�f�j
            var temp = letters[i]; // ��ԍŌ�̗v�f�����m�ہitemp�j�ɂ����
            letters[i] = letters[j]; // �����_���v�f����ԍŌ�ɂ����
            letters[j] = temp; // ���m�ۂ��������_���v�f�ɏ㏑��
        }

        for (int i = 0; i < letters.Length; i++)
        {
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = letters[i] + "";
        }
    }

}
