using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class RegistManager : MonoBehaviour
{
    [Header("UI")]
    public Text messageText;
    public InputField userNameInput;
    public InputField emailInput;
    public InputField passwordInput;

    public void RegisterButton()
    {
        if(passwordInput.text.Length < 6)
        {
            messageText.text = "Password too short!";
            return;
        }

        PlayFabClientAPI.AddUsernamePassword(new AddUsernamePasswordRequest
        {
            Username = userNameInput.text,
            Email = emailInput.text,
            Password = passwordInput.text
        }, result =>
        {
            messageText.text = "Registered and Logged in!";
            PlayFabAuthService.Instance.SaveCustomId();
            SceneManager.LoadScene(1);
        }, error =>
        {
            Debug.LogError(error.GenerateErrorReport());
            messageText.text = "error!";

            switch (error.Error)
            {
                case PlayFabErrorCode.InvalidEmailOrPassword:
                    break;

                default:
                    messageText.text = "原因不明のエラーが発生しました。\n恐れ入りますが運営にお問い合わせください。";
                    break;
            }
        });
    }

    //void OnRegisterSuccess(RegisterPlayFabUserResult result)
    //{
    //    messageText.text = "Registered and Logged in!";
    //    SceneManager.LoadScene("Welcom");
    //}
}