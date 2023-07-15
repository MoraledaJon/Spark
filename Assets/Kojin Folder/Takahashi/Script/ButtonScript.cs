using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    [SerializeField]
    GameObject pausePanel;

    private void Start()
    {
        pausePanel.SetActive(false);
    }
    void Update()
    {
     
    }

    public void OnClick()
    {
        pausePanel.SetActive(true);
    }

}
