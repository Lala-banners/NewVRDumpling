using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DumplingSceneLoader : MonoBehaviour
{
    public Button startGameButton;
    public Button quitGameButton;
    public Button settingsButton, returnButton;
    public string LoadScene = "ArtScene";

    private void Start()
    {
        startGameButton.onClick.AddListener(ChangeScene);
        
        quitGameButton.onClick.AddListener(QuitGame);
        
        settingsButton.onClick.Invoke();
        returnButton.onClick.Invoke();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(LoadScene);
    }
    
    public void QuitGame()
    {
        Debug.Log("Quitting Game");

        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #endif
        Application.Quit();
    }
}
