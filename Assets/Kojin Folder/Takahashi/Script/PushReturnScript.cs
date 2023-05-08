using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PushReturnScript : MonoBehaviour
{
    [SerializeField] 
    private GameObject[] _Card;
    private int _selected = 0;
    // Start is called before the first frame update
    void Start()
    {
         // 3秒かけて(5,0,0)へ移動する
        //this.transform.DOMove(new Vector3(5f, 0f, 0f), 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
