using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageScript : MonoBehaviour
{
    [SerializeField] 
    private GameObject[] _ball;
    private int _selected = 0;
    void Start()
    {
        _selected = PlayerPrefs.GetInt("BallSelect", 0);
        _ball[_selected].SetActive(true);
    }
    public void Right()
    {
        _ball[_selected].SetActive(false);
        _selected++;
        if (_selected >= _ball.Length)
            _selected = 0;
        _ball[_selected].SetActive(true);
    }
    public void Left()
    {
        _ball[_selected].SetActive(false);
        _selected--;
        if (_selected < 0)
            _selected = _ball.Length - 1;
        _ball[_selected].SetActive(true);
    }
    public void SelectBall()
    {
        PlayerPrefs.SetInt("BallSelect", _selected);
        SceneManager.LoadScene("GameScene");
    }
    
}