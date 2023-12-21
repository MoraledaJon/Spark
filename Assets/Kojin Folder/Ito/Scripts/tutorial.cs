using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorial : MonoBehaviour
{
    public Text novelText; //�\���p�e�L�X�g
    public string[] sentences; //�\�����������͂̔z��
    public string[] sentences2; //�\�����������͂̔z��
    public string[] sentences3; //�\�����������͂̔z��
    public GameObject nextBtn; //���{�^��
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
                if (novelText.text == sentences[i]) //���͂�����������A�{�^����\��
                {
                    nextBtn.SetActive(true);
                    if (i == (sentences.Length - 1)) //����ɍŌ�̕��͂Ȃ�{�^����\��
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
                if (novelText.text == sentences2[i]) //���͂�����������A�{�^����\��
                {
                    nextBtn.SetActive(true);
                    if (i == (sentences2.Length - 1)) //����ɍŌ�̕��͂Ȃ�{�^����\��
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
        foreach (char x in arr[i].ToCharArray()) //�`.ToCharArray()�̓e�L�X�g1�������̔z��
        {
            novelText.text += x; //1�����ǉ�
            yield return new WaitForSeconds(0.05f); //0.1�b�Ԋu
        }
    }
   
    public void OnClickNext()
    {
        if (manager.cstate == 0) //���̕�������J�n
        {
            if (i < sentences.Length - 1) //�Ō�̕��͂łȂ��Ȃ�
            {
                i++;
                novelText.text = ""; //���݂̕��͂𔒎��� ]
                StartCoroutine(DisplaySentence(sentences));
            }
        }
        else
        {
            if (i < sentences2.Length - 1) //�Ō�̕��͂łȂ��Ȃ�
            {
                i++;
                novelText.text = ""; //���݂̕��͂𔒎��� ]
                StartCoroutine(DisplaySentence(sentences2));
            }
        }     
    }
}
