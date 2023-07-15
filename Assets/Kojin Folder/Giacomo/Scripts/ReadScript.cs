using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using TMPro;
using UnityEngine.Windows;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using Unity.Mathematics;
using System.Xml.Schema;

public class ReadScript : MonoBehaviour
{
    public Image spriteRenderer;
    public Sprite sprite;
    public Sprite sprite2;
    public TextMeshProUGUI txt;
    public GameObject prefbutton;
    public GameObject prefpanel;
    public Transform btnpanel;
    public Transform btnpanel2;
    public GameObject endpanl;
    bool choice = false;
    string key = "";
    string nextLine = "";
    PreloadManager manager;
    bool preload = true;
    bool canclick = true;
    

    private void Start()
    {
        manager = GameObject.Find("PreloadManager").GetComponent<PreloadManager>();
        StartCoroutine(CoFadeIn()); 
        prefpanel.SetActive(false);
        if(manager.preload)
            spriteRenderer.sprite = sprite2;
        else
            spriteRenderer.sprite = sprite;

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
        ReadString();
    }
    private void ReadString()
    {
        StopAllCoroutines();
        StartCoroutine(CoWrite(manager.reader.ReadLine())); 
    }
    private void Update()
    {
        if(UnityEngine.Input.GetMouseButtonDown(0) || UnityEngine.Input.GetKeyDown(KeyCode.Space)) 
        {
            if(!choice && canclick)
            ReadString();
        }
    }

    private void Choose(string s,bool nyt)
    {
        key = (s.Split('[', ']')[1]);
        for(int i = 0; i<btnpanel.childCount; i++) 
        {
            Destroy(btnpanel.GetChild(i).gameObject);
        }
        choice = false;
        if(nyt)
        ReadString();
        else
        {
            StopAllCoroutines();
            StartCoroutine(CoWrite((nextLine)));
        }

    }
    IEnumerator CoWrite(string s)
    {
        string ns = "";
        bool d = CheckForTags(ref s,txt);

        if (s[0] == '$')  //�I��������
        {
            nextLine = manager.reader.ReadLine();
            choice = true;
            if (nextLine[0] == '$')  //��̑I��������
            {
                GameObject button1 = Instantiate(prefbutton, btnpanel);
                button1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = s.TrimStart('$').Substring(0,s.Length - 4);
                button1.GetComponent<Button>().onClick.AddListener(delegate { Choose(s,true); });
                GameObject button2 = Instantiate(prefbutton, btnpanel);
                button2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = nextLine.TrimStart('$').Substring(0,nextLine.Length -4);
                button2.GetComponent<Button>().onClick.AddListener(delegate { Choose(nextLine, true); });
                prefpanel.SetActive(true);
                button1.GetComponent<Button>().onClick.AddListener(OnClickButton);
                button2.GetComponent<Button>().onClick.AddListener(OnClickButton);
            }
           else
            {
                GameObject button = Instantiate(prefbutton, btnpanel);
                button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = s.TrimStart('$').Substring(0,s.Length - 4);
                button.GetComponent<Button>().onClick.AddListener(delegate { Choose(s,false); });
            }
        }
        else 
        {

            if (s.Contains('[')) //starting key
            {
                if ((s.Split('[', ']')[1]) == key) //check the key
                {
                    s = s.Remove(0, 3);
                    foreach (char c in s)
                    {
                        ns += c;
                        txt.text = ns;
                        yield return new WaitForSeconds(0.05f);
                    }
                }
                else
                {
                    ReadString();
                }
            }
            else if(d)//no starting key
            {
                foreach (char c in s)
                {
                    ns += c;
                    txt.text = ns;
                    yield return new WaitForSeconds(0.05f);
                }
            }
        }
        
    }

    private bool CheckForTags(ref string s,TextMeshProUGUI t)
    {
        bool showtext = true;
        if (s.Contains("!"))
        {
            s = s.Trim('!');
            txt.fontStyle = FontStyles.Bold;
        }
        else
        {
            txt.fontStyle = FontStyles.Normal;
        }
        if (s.Contains("Endprologue"))
        {
            spriteRenderer.sprite = sprite;
            manager.preload = false;
            showtext = false;
        }
        if (s.Contains("Startprologue"))
        {
            spriteRenderer.sprite = sprite2;
            manager.preload = true;
            showtext = false;
        }
        if (s.Contains("end"))
        {
            endpanl.SetActive(true);
            showtext = false;
        }
        //if (s.Contains("load_minigame"))
        //{
        //    int i = UnityEngine.Random.Range(0, manager.levels.Count);
        //    int lvlnum = manager.levels[i];
        //    manager.levels.RemoveAt(i);
        //    s = " ";
        //    StartCoroutine(ChangeScene(UnityEngine.Random.Range(14,18)));
        //}
        if (s.Contains("load_home"))
        {
            showtext = false;
            StartCoroutine(ChangeScene(2));
        }
        if (s.Contains("minigame"))
        {
            showtext = false;

            if (manager.cstate == 0)
            StartCoroutine(ChangeScene(14));
           else
            StartCoroutine(ChangeScene(16));
        }
        
        return showtext;

    }

    IEnumerator ChangeScene(int i)
    {
        canclick = false;
        Image FadeOut = GameObject.Find("FadeOut").GetComponent<Image>();
        Color tmpc = FadeOut.color;
        while(FadeOut.color.a < 1)
        {
            tmpc.a += 0.005f;
            FadeOut.color = tmpc;
            yield return 0;
        }
        SceneManager.LoadScene(i);
    }

    public void OnClickButton()
    { 
        prefpanel.SetActive(false);
        
    }

}