using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class TitleManager : MonoBehaviour
{
    void Start()
    {
        PlayFabAuthService.Instance.Authenticate(Authtypes.Silent);
    }
    void OnEnable()
    {
        // 先に以下の一行を実行しておくことで匿名ログイン時にWebGLでもCustomIdが維持できるようになる
        PlayFabAuthService.Instance.SaveCustomId();
        PlayFabAuthService.OnLoginSuccess += PlayFabLogin_OnLoginSuccess;
    }
    private void PlayFabLogin_OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Login Success!");
    }
    private void OnDisable()
    {
        PlayFabAuthService.OnLoginSuccess -= PlayFabLogin_OnLoginSuccess;
    }

    public void ClickTapToStart(int n){
        SceneManager.LoadScene(n);
    }
}
