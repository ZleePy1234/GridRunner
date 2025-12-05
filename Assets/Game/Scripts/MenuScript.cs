using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject loginCanvas;
    public GameObject signUpCanvas;
    public GameObject loginSignUpScreen;
    public GameObject playButton;
    private SignUpScript loginScript;

    void Awake()
    {
        loginScript = GameObject.FindWithTag("PlayFabManager").GetComponent<SignUpScript>();
    }


    public void ToggleMenu()
    {
        loginSignUpScreen.SetActive(!loginSignUpScreen.activeSelf);
        loginCanvas.SetActive(false);
        signUpCanvas.SetActive(false);
        if(loginScript.hasLoggedIn == true)
        {
            playButton.SetActive(true);
        }
        else
        {
            playButton.SetActive(false);
        }
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

    public void LoadNextSceneAsync()
    {
        StartCoroutine(LoadNextSceneCoroutine());
    }

    private IEnumerator LoadNextSceneCoroutine()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning("MenuScript: No next scene in build settings.");
            yield break;
        }

        AsyncOperation op = SceneManager.LoadSceneAsync(nextIndex);
        // allowSceneActivation left true so scene activates automatically when done
        while (!op.isDone)
        {
            // optional: use op.progress (0..0.9) for progress UI
            yield return null;
        }
    }
}
