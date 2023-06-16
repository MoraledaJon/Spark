using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PairCardScript : MonoBehaviour
{
    struct Card
    {
        public bool flipped;
        public Sprite img;
    }
    List<Card> cards = new List<Card>();
    public List<Sprite> images = new List<Sprite>();
    GameObject[] objcards;
    GameObject openCard;
    int opencards = 0;
    public GameObject prefCard;
    public GameObject timer;
    int GotCards = 0;
    public Animator animator;
    public Transform deckpos;
    PreloadManager manager;
    float sec = 120;

    void Start()
    {
        manager = GameObject.Find("PreloadManager").GetComponent<PreloadManager>(); 
        StartCoroutine(CoFadeIn());  
    }

    private void StartGame()
    {
        objcards = GameObject.FindGameObjectsWithTag("card");
        //double each image in the list
        int lenght = images.Count;
        for (int i = 0; i < lenght; i++)
        {
            images.Add(images[i]);
        }

        StartCoroutine(animdeck());
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
        StartGame();
    }
    IEnumerator animdeck()
    {
        int i = 0;
        while (i < 12)
        {
            GameObject c = Instantiate(prefCard, deckpos);
            if (i == 11)
                deckpos.GetChild(0).gameObject.SetActive(false);
            Card card = new Card();
            card.flipped = false;
            int rndImage = Random.Range(0, images.Count);
            card.img = images[rndImage];
            c.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = images[rndImage];
            images.RemoveAt(rndImage);
            cards.Add(card);
            c.GetComponent<Button>().onClick.AddListener(delegate { FlipCard(c); });

            while (Vector2.Distance(c.transform.position, objcards[i].transform.position)>0.2f)
            {
                c.transform.position = Vector2.MoveTowards(c.transform.position, objcards[i].transform.position, 3000 * Time.deltaTime);
                yield return 0;
            }
            i++;

        }

        timer.SetActive(true);

    }
    public void FlipCard(GameObject card)
    {
        if (opencards < 2)
        {
            StartCoroutine(CoFlip(card));
            opencards++;
        }
       
    }

    IEnumerator CoFlip(GameObject card)
    {
        GameObject cardface = card.transform.GetChild(0).transform.GetChild(0).gameObject;
        while (card.transform.rotation.y <= 0.99f)
        {
            card.transform.Rotate(Vector3.up * Time.deltaTime * 400);
            if (card.transform.rotation.y > 0.7f && cardface.activeSelf == false)
                cardface.SetActive(true);
            yield return null;
        }
        if(openCard != null)
        {
            if(cardface.GetComponent<Image>().sprite != openCard.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().sprite)
            {
    
                yield return new WaitForSeconds(0.25f);
                StartCoroutine(CoFlipBack(card));
                StartCoroutine(CoFlipBack(openCard));
                openCard = null;     
            }
            else
            {
                opencards-= 2;
                openCard = null;
                GotCards += 2;
                manager.Talk(0);
                if (GotCards == 12)
                    StartCoroutine(CoWin());
            }
        }
        else
            openCard = card;

    }

    IEnumerator CoWin()
    {
        animator.SetTrigger("gameclear");
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
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        if(timer.activeSelf)
        sec -= Time.deltaTime;
        timer.GetComponent<TextMeshProUGUI>().text = sec.ToString("F0");

    }

    IEnumerator CoFlipBack(GameObject card)
    {
        GameObject cardface = card.transform.GetChild(0).transform.GetChild(0).gameObject;
        while (card.transform.rotation.y >= 0)
        {;
            card.transform.Rotate(Vector3.down * Time.deltaTime * 400);
            if (card.transform.rotation.y < 0.7f && cardface.activeSelf == true)
                cardface.SetActive(false);
            yield return null;
        }
        openCard = null;
        manager.Talk(1);
        opencards--;
    }
}
