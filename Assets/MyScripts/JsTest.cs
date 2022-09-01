using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class JsTest : MonoBehaviour
{

    //[DllImport("__Internal")]
    //private static extern void Hello();

    //[DllImport("__Internal")]
    //private static extern void HelloString(string str);

    //[DllImport("__Internal")]
    //private static extern void PrintFloatArray(float[] array, int size);

    //[DllImport("__Internal")]
    //private static extern int AddNumbers(int x, int y);

    //[DllImport("__Internal")]
    //private static extern string StringReturnValueFunction();

    //[DllImport("__Internal")]
    //private static extern void BindWebGLTexture(int texture);

    //[DllImport("__Internal")]
    //private static extern string TestIndexedDB();

    // スタート時に呼ばれる
    void Start()
  {
        // 関数呼び出し
        //Hello();

        //// 数値型の引数と戻り値
        //int result = AddNumbers(5, 7);
        //Debug.Log(result);

        //// 数値型以外の型の引数
        //float[] myArray = new float[10];
        //PrintFloatArray(myArray, myArray.Length);

        //// 文字列型の引数
        //HelloString("This is a string.");

        // 文字列の戻り値
        //string text = TestIndexedDB();
        //Debug.Log(text);
        //SetText(text);

        // WebGLテクスチャのバインド
        //var texture = new Texture2D(0, 0, TextureFormat.ARGB32, false);
        //BindWebGLTexture(texture.GetNativeTextureID());

        //PlayerPrefs.SetString("testKey", "testValue");

        //string text = PlayerPrefs.GetString("testKey", "No such data.");
        //SetText(text);
    }

    public void SetText(string str)
    {
        Text text = GameObject.Find("TextFromIndexedDB").GetComponent<Text>();
        text.text = str;
    }

}