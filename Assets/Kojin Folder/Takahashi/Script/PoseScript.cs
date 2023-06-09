using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PoseScript : MonoBehaviour

{
    //ボタン押したら別のsceneに行く
    public void PoseStart()
    {
        SceneManager.LoadScene("HomeScene");
    }
}
