using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JetBrains.Annotations;
using System;
using System.Security.Cryptography;

public class PushReturnScript : MonoBehaviour

    
{
    [SerializeField]
    private Transform FrontHolder;
    [SerializeField]
    private Transform BackHolder;

    [SerializeField]
    private List<GameObject> cards;

    [SerializeField]
    private Transform[] pos;
    private int[] indexArr = {1,2,3,4,5};

    public List<int> layerOrder; // レイヤー順のリスト

    void Start()
    {

        // シャッフル後のカードのレイヤー順を設定
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].GetComponent<SpriteRenderer>().sortingOrder = layerOrder[i];
        }
    }


    public void OnClick()
    {
        for (int i = 0; i < 5; i++)
        {
            moveCard(cards[i], ref indexArr[i]);
        }
    }

    public void OnClicklx()
    {
        
        for (int i = 0; i < 5; i++)
        {
            moveCardback(cards[i], ref indexArr[i]);
        }
    }


    private void moveCardback(GameObject card, ref int x)
    {
        if (x <= 0)
            x = 5;
        card.transform.DOMove(pos[--x].position, 1f)
            .OnComplete(() => {
                 // カードの並び順を指定した場所にカードをぶち込むことで
                 // 手裏側に表示されるように移動する
                 SortCardLayer();
             });
    }
    private void moveCard(GameObject card, ref int x)
    {
        if (x >= 5)
            x = 0;
        card.transform.DOMove(pos[x++].position, 1f)
            .OnComplete(() => {
                // カードの並び順を指定した場所にカードをぶち込むことで
                // 手表側に表示されるように移動する
                SortCardLayer();
            });
        
    }


    void SortCardLayer()
    {

        // カードの順序を指定したレイヤー順に並び替える
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].GetComponent<SpriteRenderer>().sortingOrder = layerOrder[i];
        }

    }



    /*
     
    void SortCardLayer()
    {
              for (int i = 0; i < 5; i++)
              { 
                if(Cards[i].transform.localPosition.y < 30)
              {
                  Cards[i].transform.SetParent(FrontHolder);
              }
              else
              {
                  Cards[i].transform.SetParent(BackHolder);
              }
          
              }
 
     
     */
}
     

 
   
