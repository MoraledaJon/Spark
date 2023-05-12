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
            if (Input.GetKeyDown(KeyCode.P))
            {
                Time.timeScale = 0;
                pausePanel.SetActive(true);
            }
        }
    }
