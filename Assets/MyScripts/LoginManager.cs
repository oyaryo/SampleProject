using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class LoginManager : MonoBehaviour
{
    [Header("UI")]
    public Text messageText;
    public InputField emailInput;
    public InputField passwordInput;

    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    void OnError(PlayFabError error)
    {
        messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }

    void OnLoginSuccess(LoginResult result)
    {
        messageText.text = "Logged in!";
        Debug.Log("Successful login/account create!");
        
        // ログインが成功したらPlayerPrefsのPLAYFAB_CUSTOM_IDを回復可能ログインのCustomIdへ書き換える
        ChangeCustomId();
        SceneManager.LoadScene(1);
    }

    // CustomIdを取得するための関数
    void ChangeCustomId(){
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest
        {

        },
        result => {
            PlayerPrefs.SetString("PLAYFAB_CUSTOM_ID", result.AccountInfo.CustomIdInfo.CustomId);
            Debug.Log(result.AccountInfo.CustomIdInfo.CustomId);
        },
        error => {
            Debug.Log("error");
        });
    }
}
