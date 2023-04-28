using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScriot : MonoBehaviour
{
        // ボタンが押された場合、今回呼び出される関数
public void OnClick()
{
      // 移動させたいオブジェクトを取得
      GameObject obj = GameObject.Find("PlayerObject");
      // オブジェクトを移動
      obj.transform.Translate(1.0f,0.0f,0.0f);
}
    }
