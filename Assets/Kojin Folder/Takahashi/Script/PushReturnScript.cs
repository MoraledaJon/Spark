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
    private List<GameObject> Cards;

    [SerializeField]
    private Transform[] pos;
    private int[] indexArr = {1,2,3,4,5};

    private List<int> _rightPattern = new List<int>()
    {
        3,4,2,1,5
    };
    
    public void OnClick()
    {
        for (int i = 0; i < 5; i++)
        {
            moveCard(Cards[i], ref indexArr[i]);
        }
    }

    public void OnClicklx()
    {
        
        for (int i = 0; i < 5; i++)
        {
            moveCardback(Cards[i], ref indexArr[i]);
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
              
        //      for (int i = 0; i < 5; i++)
        //  {
        //      if(Cards[i].transform.localPosition.y < 30)
        //      {
        //          Cards[i].transform.SetParent(FrontHolder);
        //      }
        //      else
        //      {
        //          Cards[i].transform.SetParent(BackHolder);
        //      }
        //  }
     }
     

 }
   
