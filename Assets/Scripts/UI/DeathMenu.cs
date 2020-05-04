using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{

    public void Retry() {
        SceneManager.LoadScene(2);
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene(0);
    }

    public void Quit() {
        Application.Quit();
    }
}
