using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public CanvasGroup mainPanel;
    public CanvasGroup helpPanel;

    public void Start() {

    }

    public void switchToMain() {
        helpPanel.alpha = 0f;
        helpPanel.interactable = false;
        helpPanel.blocksRaycasts = false;

        mainPanel.alpha = 1f;
        mainPanel.interactable = true;
        mainPanel.blocksRaycasts = true;
    }

    public void switchToHelp() {
        helpPanel.alpha = 1f;
        helpPanel.interactable = true;
        helpPanel.blocksRaycasts = true;

        mainPanel.alpha = 0f;
        mainPanel.interactable = false;
        mainPanel.blocksRaycasts = false;
    }

    public void startGame() {
        SceneManager.LoadScene(2);
    }

    public void Quit() {
        Application.Quit();
    }
    
}
