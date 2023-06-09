using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoujiManager : MonoBehaviour
{
    public TextMeshProUGUI consoletxt;
    bool animOver = false;
    public GameObject[] levelpanels;
    [HideInInspector]
    public byte setfolders = 0;
    public Animator gcanim;
    public TextMeshProUGUI timettxt;
    public GameObject prefFolder;
    private float timer = 30;
    private bool timermove = true;
    [HideInInspector]
    public int nFolder;
    [HideInInspector]
    public int nViruses;
    PreloadManager manager;
    byte currentlevel = 0;

    private void Start()
    {
        manager = GameObject.Find("PreloadManager").GetComponent<PreloadManager>();
        StartCoroutine(CoFadeIn());
    }

    IEnumerator CoFadeIn()
    {
        Image FadeOut = GameObject.Find("FadeOut").GetComponent<Image>();
        Color tmpc = FadeOut.color;
        tmpc.a = 1;
        FadeOut.color = tmpc;
        while (FadeOut.color.a > 0)
        {
            tmpc.a -= 0.005f;
            FadeOut.color = tmpc;
            yield return 0;
        }

        //start level
        GoToLevel(0);
        nViruses = 7;
    }
    IEnumerator CoLevelCompleted(byte level)
    {
        currentlevel = level;
        timermove = false;
        animOver = false;
        manager.Talk(0);
        StartCoroutine(CoLoadLevel(null, "完了しました - \r\n...\r\n"));
        yield return new WaitForSeconds(2);
        GoToLevel(level);
    }
    IEnumerator CoLevelFailed(byte level)
    {
        timermove = false;
        animOver = false;
        manager.Talk(1);
        StartCoroutine(CoLoadLevel(null, "エラー - \r\n...\r\n"));
        yield return new WaitForSeconds(2);
        currentlevel++;
        GoToLevel(++level);
    }

    public void LevelCompleted(byte level) 
    {
        StartCoroutine(CoLevelCompleted(level));
    }
    public void GoToLevel(byte level)
    {
        StopAllCoroutines();
        animOver = false;
        timer = 30;
        timermove = true;
        switch(level) 
        {
                case 0:
                StartCoroutine(CoLoadLevel(levelpanels[0], "全てのファイルを削除する\r\n"));
                break;
                case 1:
                StartCoroutine(CoLoadLevel(levelpanels[1], "/documents/spark\r\n隙間にフォルダーを入れる\r\n"));
                break;
                case 2:
                StartCoroutine(CoLoadLevel(levelpanels[2], "documents/spark/ai\r\n隙間にフォルダーを入れる\r\n"));
                break;
                case 3:
                StartCoroutine(CoLoadLevel(levelpanels[3], "/???/??\r\n破損したファイルを削除する\r\n"));
                break;
        }
    }
    IEnumerator CoLoadLevel(GameObject panel, string dintext)
    {
        if (consoletxt != null)
        {
            foreach (GameObject go in levelpanels) {go.SetActive(false);}
            if(panel!=null)
            panel.SetActive(true);
            consoletxt.text = "";

            foreach (char c in dintext)
            {
                consoletxt.text += c;
                yield return new WaitForSeconds(0.05f);
            }
            if (panel != null)
            {
                animOver = true;
                StartCoroutine(AnimConsoleText());
            }
        }

    }

    private void Update()
    {
        if (timermove)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timer = 0;
                StartCoroutine(CoLevelFailed(currentlevel));
            }
        }
        timettxt.text = "00:" + timer.ToString("F0");
    }

    public void GameClear()
    {
        StartCoroutine(CoWin());
    }
    IEnumerator CoWin()
    {
        gcanim.SetTrigger("gameclear");
        yield return new WaitForSeconds(3);
        Image FadeOut = GameObject.Find("FadeOut").GetComponent<Image>();
        Color tmpc = FadeOut.color;
        while (FadeOut.color.a < 1)
        {
            tmpc.a += 0.005f;
            FadeOut.color = tmpc;
            yield return 0;
        }
        PreloadManager manager = GameObject.Find("PreloadManager").GetComponent<PreloadManager>();
        SceneManager.LoadScene(6);
    }
    IEnumerator AnimConsoleText()
    {
        while (animOver )
        {
            string starts = consoletxt.text;
            for (int i = 0; i < 3; i++)
            {
                if (!animOver)
                    break;
                consoletxt.text += ".";
                yield return new WaitForSeconds(1);
            }
            if (animOver)
                consoletxt.text = starts;
        }

    }

}
