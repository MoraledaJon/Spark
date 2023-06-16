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

public class ReadScript : MonoBehaviour
{

    public TextMeshProUGUI txt;
    public GameObject prefbutton;
    public Transform btnpanel;
    bool choice = false;
    string key = "";
    string nextLine = "";
    PreloadManager manager;
    bool canclick = true;
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
        CheckForTags(ref s,txt);

        if (s[0] == '$')  //‘I‘ðŽˆ‚ ‚é
        {
            nextLine = manager.reader.ReadLine();
            choice = true;
            if (nextLine[0] == '$')  //“ñ‚Â‚Ì‘I‘ðŽˆ‚ ‚é
            {
                GameObject button1 = Instantiate(prefbutton, btnpanel);
                button1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = s.TrimStart('$').Substring(0,s.Length - 4);
                button1.GetComponent<Button>().onClick.AddListener(delegate { Choose(s,true); });
                GameObject button2 = Instantiate(prefbutton, btnpanel);
                button2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = nextLine.TrimStart('$').Substring(0,nextLine.Length -4);
                button2.GetComponent<Button>().onClick.AddListener(delegate { Choose(nextLine, true); });
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
            else //no starting key
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

    private void CheckForTags(ref string s,TextMeshProUGUI t)
    {
        if (s.Contains("!"))
        {
            s = s.Trim('!');
            txt.fontStyle = FontStyles.Bold;
        }
        else
        {
            txt.fontStyle = FontStyles.Normal;
        }
        if(s.Contains("load_minigame"))
        {
            int i = UnityEngine.Random.Range(0, manager.levels.Count);
            int lvlnum = manager.levels[i];
            manager.levels.RemoveAt(i);
            s = " ";
            StartCoroutine(ChangeScene(lvlnum));
        }
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
 

}