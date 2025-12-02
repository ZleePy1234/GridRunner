using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject loginCanvas;
    public GameObject signUpCanvas;
    public GameObject loginSignUpScreen;
    public GameObject playButton;
    private LoginScript loginScript;

    void Awake()
    {
        loginScript = GameObject.FindWithTag("PlayFabManager").GetComponent<LoginScript>();
    }


    public void ToggleMenu()
    {
        loginSignUpScreen.SetActive(!loginSignUpScreen.activeSelf);
        loginCanvas.SetActive(false);
        signUpCanvas.SetActive(false);
        playButton.SetActive(loginScript.hasLoggedIn);
    }
    public void ToggleLoginCanvas()
    {
        loginCanvas.SetActive(!loginCanvas.activeSelf);
        signUpCanvas.SetActive(false);
        loginSignUpScreen.SetActive(false);
    }
    public void ToggleSignUpCanvas()
    {
        signUpCanvas.SetActive(!signUpCanvas.activeSelf);
        loginCanvas.SetActive(false);
        loginSignUpScreen.SetActive(false);
    }
}
