using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textchange : MonoBehaviour
{
	public Text Changetext;

	public static int p = 0;

    private void Start()
    {
		if (p >= 1)
		{
			Changetext.GetComponent<Text>().text = "１章";
		}
		else
        {
			Changetext.GetComponent<Text>().text = "練習";
		}
		
	}
    //ボタンのテキストを切り替える
    public void ChangeText()
	{
		p++;
		
	}
}
