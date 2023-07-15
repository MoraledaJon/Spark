using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Animator button1Anim;
    public Animator button2Anim;
    public Animator button3Anim;
    public Animator button4Anim;
    public Animator button5Anim;

    public List<Button>  buttons;

    public string result;

    private int clickCount = 0;

    public MiniGameManager miniGameManager;
    public Animator winAnimator;

    int stageNumber = 0;

    public List<Transform> stageNumberTransform;
    public Image currentStageImgage;
    PreloadManager pmanager;

    public void Start()
    {
        pmanager = GameObject.Find("PreloadManager").GetComponent<PreloadManager>();
        currentStageImgage.transform.position = stageNumberTransform[stageNumber].position;
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
    }

    public void Button1Animation()
    {
        button1Anim.SetBool("isClick", true);
        result += buttons[0].GetComponentInChildren<TextMeshProUGUI>().text;
        buttons[0].enabled = false;
        clickCount++;
        check();
    }

    public void Button2Animation()
    {
        button2Anim.SetBool("isClick", true);
        result += buttons[1].GetComponentInChildren<TextMeshProUGUI>().text;
        buttons[1].enabled = false;
        clickCount++;
        check();
    }

    public void Button3Animation()
    {
        button3Anim.SetBool("isClick", true);
        result += buttons[2].GetComponentInChildren<TextMeshProUGUI>().text;
        buttons[2].enabled = false;
        clickCount++;
        check();
    }

    public void Button4Animation()
    {
        button4Anim.SetBool("isClick", true);
        result += buttons[3].GetComponentInChildren<TextMeshProUGUI>().text;
        buttons[3].enabled = false;
        clickCount++;
        check();
    }

    public void Button5Animation()
    {
        button5Anim.SetBool("isClick", true);
        result += buttons[4].GetComponentInChildren<TextMeshProUGUI>().text;
        buttons[4].enabled = false;
        clickCount++;
        check();
    }


    private void check()
    {
        if (clickCount == 5)
        {
            if (result == miniGameManager.word)
            {
                WinOrLose(true);
                //miniGameManager.timerIsRunning = false;
            }
            else
            {
                button1Anim.SetBool("isClick", false);
                button2Anim.SetBool("isClick", false);
                button3Anim.SetBool("isClick", false);
                button4Anim.SetBool("isClick", false);
                button5Anim.SetBool("isClick", false);
                buttons[0].enabled = true;
                buttons[1].enabled = true;
                buttons[2].enabled = true;
                buttons[3].enabled = true;
                buttons[4].enabled = true;
                clickCount = 0;
                result = "";
            }
        }
    }

    IEnumerator CoWin()
    {
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
        SceneManager.LoadScene(18);
    }
    public void WinOrLose(bool result1)
    {
        if(result1)
        {
            if(stageNumber != 4)
            {
                pmanager.Talk(0);
                StartCoroutine(WinShuffle());
                
            }else
            {
                winAnimator.SetTrigger("win");
                buttons[0].enabled = false;
                buttons[1].enabled = false;
                buttons[2].enabled = false;
                buttons[3].enabled = false;
                buttons[4].enabled = false;
                miniGameManager.timerIsRunning = false;
                //win
                StartCoroutine(CoWin());    

            }
        }
        else
        {
            pmanager.Talk(1);
            winAnimator.SetTrigger("lose");
            buttons[0].enabled = false;
            buttons[1].enabled = false;
            buttons[2].enabled = false;
            buttons[3].enabled = false;
            buttons[4].enabled = false;
        }
    }

    IEnumerator WinShuffle()
    {
        miniGameManager.timerIsRunning = false;

        yield return new WaitForSeconds(0.5f);
        
        button1Anim.SetBool("isClick", false);
        button2Anim.SetBool("isClick", false);
        button3Anim.SetBool("isClick", false);
        button4Anim.SetBool("isClick", false);
        button5Anim.SetBool("isClick", false);

        yield return new WaitForSeconds(0.5f);

        miniGameManager.Shuffle(miniGameManager.word);

        clickCount = 0;
        result = "";
        stageNumber++;
        miniGameManager.timeRemaining = 10f;
        miniGameManager.timerIsRunning = true;
        currentStageImgage.transform.position = stageNumberTransform[stageNumber].position;

        buttons[0].enabled = true;
        buttons[1].enabled = true;
        buttons[2].enabled = true;
        buttons[3].enabled = true;
        buttons[4].enabled = true;
    }
}
