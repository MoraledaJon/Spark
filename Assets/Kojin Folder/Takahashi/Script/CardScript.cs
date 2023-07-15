using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardScript : MonoBehaviour
{
    PreloadManager manager;
    private void Start()
    {
        manager = GameObject.Find("PreloadManager").GetComponent<PreloadManager>();
    }
    public void DirEasy()
    {
        //SceneManager.LoadScene(UnityEngine.Random.Range(14, 18));
        if (manager.cstate == (PreloadManager.state)0)
        {
            SceneManager.LoadScene(14);
        }
        else
            SceneManager.LoadScene(1);
    }

}
