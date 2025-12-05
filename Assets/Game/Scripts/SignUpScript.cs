 using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;

public class SignUpScript : MonoBehaviour
{
    public TMP_InputField registerEmailInput;
    public TMP_InputField registerPasswordInput;
    public TMP_InputField confirmPasswordInput;
    public TMP_InputField registerUsernameInput;
    public TMP_InputField loginEmailInput;
    public TMP_InputField loginPasswordInput;

    public TMP_InputField avatarUrlInput;
    public TextMeshProUGUI loginMessageText;
    public TextMeshProUGUI registerMessageText;
    public bool hasLoggedIn = false;

    public static SignUpScript instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnRegisterButtonClicked()
    {
        if (registerPasswordInput.text != confirmPasswordInput.text)
        {
            registerMessageText.text = "Passwords no coincide";
            return;
        }

        var request = new RegisterPlayFabUserRequest
        {
            Email = registerEmailInput.text,
            Password = registerPasswordInput.text,
            Username = registerUsernameInput.text,
            RequireBothUsernameAndEmail = true
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
        
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        registerMessageText.text = "Registration successful!";
        Debug.Log("User registered successfully: " + result.PlayFabId);
    }
    private void OnRegisterFailure(PlayFabError error)
    {
        registerMessageText.text = "Registration failed: " + error.ErrorMessage;
        Debug.LogError("Error registering user: " + error.GenerateErrorReport());
    }

    public void OnLoginButtonClicked()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = loginEmailInput.text,
            Password = loginPasswordInput.text
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }
    private void OnLoginSuccess(LoginResult result)
    {
        loginMessageText.text = "Login!";
        Debug.Log("Login succesfull:" + result.PlayFabId);
        hasLoggedIn = true;
    }
    private void OnLoginFailure(PlayFabError error)
    {
        loginMessageText.text = "Login failed: " + error.ErrorMessage;
        Debug.LogError("Error: " + error.GenerateErrorReport());
    }
}
