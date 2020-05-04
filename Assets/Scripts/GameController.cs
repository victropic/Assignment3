using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject pathway;
    public GameObject towerLocations;
    public Enemy enemy;

    /* Stats */
    public float maxCastleHealth = 100f;
    public float castleHealth;
    public int castleGold = 0;

    /* States */
    bool paused = false;

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
            } else {
                Time.timeScale = 1f;
                paused = false;
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

    public void CastleTakeDamage(float amount) {
        castleHealth -= amount;
    }

    public void CastleCollectGold(int amount) {
        castleGold += amount;
    }
}
