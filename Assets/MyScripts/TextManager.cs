using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{

    //[DllImport("__Internal")]
    //private static extern void Hello();

    [DllImport("__Internal")]
    private static extern void Firestore();

    // js側から更新が合った時に呼び出される関数
    public void UpdateText(string newText)
    {
        // テキストコンポーネントの取得
        Text text = GameObject.Find("TextFromFirestore").GetComponent<Text>();
        text.text = newText;
    }

    void Start()
    {
        //js側の関数を呼び出してデータの監視開始
        Firestore();
    }
}