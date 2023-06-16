using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirusBehaviour : MonoBehaviour
{
    private Sprite sImage;
    public Sprite[] vImages;


    private void Start()
    {
        sImage = GetComponent<Image>().sprite;
        StartCoroutine(CoAnimVirus());
    }

    IEnumerator CoAnimVirus()
    {
        while (true)
        { 
            yield return new WaitForSeconds(Random.Range(2f, 6f));
            int i = 0;
            foreach(var image in vImages) 
            {
                GetComponent<Image>().sprite = vImages[i++];
                yield return new WaitForSeconds(0.085f);
            }
            GetComponent<Image>().sprite = sImage;
        }
    }
}
