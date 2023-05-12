using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story2SceneButton : MonoBehaviour
{ 
    public void OnClickToGameSceneButton()
    {
        SceneManager.LoadScene("Story2Scene");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    } 
}
