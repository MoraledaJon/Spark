using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PreloadManager : MonoBehaviour
{
    public enum state
    {
        tutorial,
        game,
        end
    }


    [HideInInspector]
    public StreamReader reader;
    string path = "Assets/Kojin Folder/Giacomo/Resources/read.txt";
    public byte success = 0;
    public byte failure = 0;
    public string[] WinPhrases;
    public string[] LosePhrases;
    public string[] HomePhrases;
    public List<byte> levels;
    public bool preload = true;
    public state cstate = state.tutorial;
    public AudioClip[] seWin;
    public AudioClip[] seLose;
    public AudioClip[] seHome;
    private AudioSource ass;
    public GameObject resultPanel;
    public Image fillImage;
    public TextMeshProUGUI percetage;
    public TextMeshProUGUI numPlatinumu;
    public byte platinum = 0;

    public Animator stageClearAnimation;


    public void ShowResutl()
    {

        resultPanel.SetActive(true);
        resultPanel.transform.parent.GetComponent<Animator>().SetTrigger("open");
        StartCoroutine(CoSho());
        
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        reader = new StreamReader(path);
        ass = GetComponent<AudioSource>();
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
        int rnd = 0;
        AudioClip ac = null; 
        switch (i) 
        {

            case 0:
                rnd = Random.Range(0, WinPhrases.Length);
                txt.text = WinPhrases[rnd];
                ac = seWin[rnd];
                ass.PlayOneShot(ac);
                break;
            case 1:
                rnd = Random.Range(0, LosePhrases.Length);
                txt.text = LosePhrases[rnd];
                ac = seLose[rnd];
                ass.PlayOneShot(ac);
                break;
            case 2:
                rnd = Random.Range(0, HomePhrases.Length);
                txt.text = HomePhrases[rnd];
                ac = seHome[rnd];
                ass.PlayOneShot(ac);
                break;
        }
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    IEnumerator CoSho()
    {
        float perch = (success * 100) / 40;
        float startp = 0;


        while(startp < perch) 
        {
            yield return new WaitForSeconds(0.025f);
            startp += 1;
            fillImage.fillAmount = startp / 100;
            percetage.text = startp.ToString() + "%";      
        }

        fillImage.fillAmount = perch / 100;
        percetage.text = perch.ToString() + "%";

        byte startPlat = 0;
        while(startPlat < platinum)
        {
            startPlat++;
            yield return new WaitForSeconds(0.2f);
            numPlatinumu.text = startPlat.ToString() + "/4";
        }

        numPlatinumu.text = platinum.ToString() + "/4";

        yield return new WaitForSeconds(3);
        resultPanel.transform.parent.gameObject.SetActive(false);
    }
    
}
