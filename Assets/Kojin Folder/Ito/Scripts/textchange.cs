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
			Changetext.GetComponent<Text>().text = "�P��";
		}
		else
        {
			Changetext.GetComponent<Text>().text = "���K";
		}
		
	}
    //�{�^���̃e�L�X�g��؂�ւ���
    public void ChangeText()
	{
		p++;
		
	}
}
