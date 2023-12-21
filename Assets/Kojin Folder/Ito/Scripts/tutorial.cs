using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorial : MonoBehaviour
{
    public Text novelText; //表示用テキスト
    public string[] sentences; //表示したい文章の配列
    public string[] sentences2; //表示したい文章の配列
    public string[] sentences3; //表示したい文章の配列
    public GameObject nextBtn; //次ボタン
    public int i;
    PreloadManager manager;
    public GameObject panel;
    public static int u;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("PreloadManager").GetComponent<PreloadManager>();

        if(manager.cstate == 0)
        StartCoroutine(DisplaySentence(sentences));
        else
        StartCoroutine(DisplaySentence(sentences2));
    }

    // Update is called once per frame
    void Update()
    {
            if (manager.cstate == 0)
            {
                if (novelText.text == sentences[i]) //文章が完成したら、ボタンを表示
                {
                    nextBtn.SetActive(true);
                    if (i == (sentences.Length - 1)) //さらに最後の文章ならボタン非表示
                    {
                        nextBtn.SetActive(false);
                        panel.SetActive(false);
                    }
                }
                else
                {
                    nextBtn.SetActive(false);
                }
            }
            else
            {
                if (novelText.text == sentences2[i]) //文章が完成したら、ボタンを表示
                {
                    nextBtn.SetActive(true);
                    if (i == (sentences2.Length - 1)) //さらに最後の文章ならボタン非表示
                    {
                        nextBtn.SetActive(false);
                        panel.SetActive(false);
                        u++;
                    }
                }
                else
                {
                    nextBtn.SetActive(false);
                }
            }
        

    }
    IEnumerator DisplaySentence(string[] arr)
    {
        foreach (char x in arr[i].ToCharArray()) //～.ToCharArray()はテキスト1文字ずつの配列
        {
            novelText.text += x; //1文字追加
            yield return new WaitForSeconds(0.05f); //0.1秒間隔
        }
    }
   
    public void OnClickNext()
    {
        if (manager.cstate == 0) //次の文字送り開始
        {
            if (i < sentences.Length - 1) //最後の文章でないなら
            {
                i++;
                novelText.text = ""; //現在の文章を白紙に ]
                StartCoroutine(DisplaySentence(sentences));
            }
        }
        else
        {
            if (i < sentences2.Length - 1) //最後の文章でないなら
            {
                i++;
                novelText.text = ""; //現在の文章を白紙に ]
                StartCoroutine(DisplaySentence(sentences2));
            }
        }     
    }
}
