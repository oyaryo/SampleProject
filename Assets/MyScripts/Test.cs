using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

  void Update()
  {
    {
      EndGame();
    }

    //ゲーム終了
    void EndGame()
    {
      //Escが押された時
      if (Input.GetKey(KeyCode.Escape))
      {

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
        Application.Quit();//ゲームプレイ終了
#endif
      }
    }
  }
}
