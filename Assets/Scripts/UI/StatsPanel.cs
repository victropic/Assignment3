using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsPanel : MonoBehaviour
{

    Text healthText;
    Text goldText;
    Text waveText;

    GameController gameController;
    EnemySpawner enemySpawner;
    
    void Start()
    {
        healthText = transform.GetChild(0).GetComponent<Text>();
        goldText = transform.GetChild(1).GetComponent<Text>();
        waveText = transform.GetChild(2).GetComponent<Text>();

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
    }

    void Update()
    {
        healthText.text = "Health: " + gameController.castleHealth.ToString();
        goldText.text = "Gold: " + gameController.castleGold.ToString();
        waveText.text = "Wave: " + enemySpawner.waveNumber.ToString() + ((enemySpawner.delay)? " (Peace)" : "" );
    }
}
