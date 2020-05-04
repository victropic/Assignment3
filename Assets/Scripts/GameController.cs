using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject pathway;
    public GameObject towerLocations;
    public Enemy enemy;
    public GameObject towerPref;
    public CanvasGroup pausePanel;

    /* Stats */
    public float maxCastleHealth = 100f;
    public float castleHealth;
    public int castleGold = 0;

    /* States */
    bool paused = false;

    /* Audio */
    public GameObject rewardSound;
    public GameObject purchaseSound;

    /* Misc */
    Camera cam;
    GameObject canvas;
    GameObject towerButton;

    void Start()
    {
        towerButton = Resources.Load("Prefabs/TowerButton") as GameObject;

        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        canvas = GameObject.FindGameObjectWithTag("MainCanvas");

        castleHealth = maxCastleHealth;

        CreateTowerLocationButtons();
    }

    void Update()
    {

        /* Pause Game */
        if(Input.GetButtonDown("Pause")) {
            if(!paused) {
                Time.timeScale = 0f;
                paused = true;
                pausePanel.alpha = 1;
                pausePanel.blocksRaycasts = true;
                pausePanel.interactable = true;
            } else {
                Time.timeScale = 1f;
                paused = false;
                pausePanel.alpha = 0;
                pausePanel.blocksRaycasts = false;
                pausePanel.interactable = false;
            }
        }
        
    }

    void CreateTowerLocationButtons() {
        for(int i = 0; i < towerLocations.transform.childCount; i++) {
            Transform loc = towerLocations.transform.GetChild(i);
            
            GameObject button = Instantiate(towerButton, canvas.transform, false);
            button.transform.position = cam.WorldToScreenPoint(loc.position);
        }
    }

    public Tower CreateTower(Vector3 position) {
        if(Purchase(50)) {
            return Instantiate(towerPref, position, Quaternion.identity).GetComponent<Tower>();
        } else {
            return null;
        }
    }

    bool Purchase(int amount) {
        if(castleGold - amount < 0) {
            return false;
        }

        castleGold -= amount;
        Instantiate(purchaseSound);
        return true;
    }


    public void CastleTakeDamage(float amount) {
        castleHealth -= amount;
        if(castleHealth <= 0) {
            SceneManager.LoadScene(1);
        }
    }

    public void CastleCollectGold(int amount) {
        Instantiate(rewardSound);
        castleGold += amount;
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene(0);
    }

    public void Quit() {
        Application.Quit();
    }
}
