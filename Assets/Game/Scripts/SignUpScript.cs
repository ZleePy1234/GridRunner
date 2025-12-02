 using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;

public class SignUpScript : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField confirmPasswordInput;
    public TMP_InputField usernameInput;

    public TMP_InputField avatarUrlInput;
    public TextMeshProUGUI messageText;

    public void OnRegisterButtonClicked()
    {
        if (passwordInput.text != confirmPasswordInput.text)
        {
            messageText.text = "Passwords no coincide";
            return;
        }

        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            Username = usernameInput.text,
            RequireBothUsernameAndEmail = true
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
        
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        messageText.text = "Registration successful!";
        Debug.Log("User registered successfully: " + result.PlayFabId);
    }
    private void OnRegisterFailure(PlayFabError error)
    {
        messageText.text = "Registration failed: " + error.ErrorMessage;
        Debug.LogError("Error registering user: " + error.GenerateErrorReport());
    }
}
