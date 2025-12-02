using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;

public class LoginScript : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TextMeshProUGUI messageText;

    public bool hasLoggedIn = false;

    public void OnLoginButtonClicked()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }
    private void OnLoginSuccess(LoginResult result)
    {
        messageText.text = "Login!";
        Debug.Log("Login succesfull:" + result.PlayFabId);
        hasLoggedIn = true;
    }
    private void OnLoginFailure(PlayFabError error)
    {
        messageText.text = "Login failed: " + error.ErrorMessage;
        Debug.LogError("Error: " + error.GenerateErrorReport());
    }
}
