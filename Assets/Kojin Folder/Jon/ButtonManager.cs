using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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



    public void Button1Animation()
    {
        button1Anim.SetBool("isClick", true);
        result += buttons[0].GetComponentInChildren<TextMeshProUGUI>().text;
        buttons[0].enabled = false;
        clickCount++;
    }

    public void Button2Animation()
    {
        button2Anim.SetBool("isClick", true);
        result += buttons[1].GetComponentInChildren<TextMeshProUGUI>().text;
        buttons[1].enabled = false;
        clickCount++;
    }

    public void Button3Animation()
    {
        button3Anim.SetBool("isClick", true);
        result += buttons[2].GetComponentInChildren<TextMeshProUGUI>().text;
        buttons[2].enabled = false;
        clickCount++;
    }

    public void Button4Animation()
    {
        button4Anim.SetBool("isClick", true);
        result += buttons[3].GetComponentInChildren<TextMeshProUGUI>().text;
        buttons[3].enabled = false;
        clickCount++;
    }

    public void Button5Animation()
    {
        button5Anim.SetBool("isClick", true);
        result += buttons[4].GetComponentInChildren<TextMeshProUGUI>().text;
        buttons[4].enabled = false;
        clickCount++;
    }

    private void Update()
    {
        if(clickCount == 5)
        {
            if (result == miniGameManager.word)
            {

                winAnimator.SetTrigger("win");
            }
            else
            {
                winAnimator.SetTrigger("lose");
            }
        }
    }
}
