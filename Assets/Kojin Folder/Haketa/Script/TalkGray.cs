using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TalkGray : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _image.color = Color.gray;
            _spriteRenderer.color = Color.gray;
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _image.color = Color.white;
            _spriteRenderer.color = Color.white;
        }
    }
}