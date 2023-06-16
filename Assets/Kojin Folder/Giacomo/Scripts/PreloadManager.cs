using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PreloadManager : MonoBehaviour
{
    [HideInInspector]
    public StreamReader reader;
    string path = "Assets/Kojin Folder/Giacomo/Resources/read.txt";
    public byte success = 0;
    public byte failure = 0;
    public string[] WinPhrases;
    public string[] LosePhrases;
    public List<byte> levels;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        reader = new StreamReader(path);
    }
    public void StartGame()
    {
        StartCoroutine(CoStartGame());
    }
    IEnumerator CoStartGame()
    {
        Image FadeOut = GameObject.Find("FadeOut").GetComponent<Image>();
        Color tmpc = FadeOut.color;
        while (FadeOut.color.a < 1)
        {
            tmpc.a += 0.005f;
            FadeOut.color = tmpc;
            yield return 0;
        }
        PreloadManager manager = GameObject.Find("PreloadManager").GetComponent<PreloadManager>();
        SceneManager.LoadScene(1);
    }

    public void Talk(byte i)
    {
        TextMeshProUGUI txt = GameObject.Find("reactiontxt").GetComponent<TextMeshProUGUI>();
        switch(i) 
        {
            case 0:
                txt.text = WinPhrases[Random.Range(0, WinPhrases.Length)];
                break;
            case 1:
                txt.text = LosePhrases[Random.Range(0, WinPhrases.Length)];
                break;
        }
    }
}
