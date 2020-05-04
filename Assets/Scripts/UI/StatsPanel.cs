using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsPanel : MonoBehaviour
{

    Text healthText;
    Text goldText;

    GameController gameController;
    
    void Start()
    {
        healthText = transform.GetChild(0).GetComponent<Text>();
        goldText = transform.GetChild(1).GetComponent<Text>();

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        healthText.text = "Health: " + gameController.castleHealth.ToString();
        goldText.text = "Gold: " + gameController.castleGold.ToString();
    }
}
