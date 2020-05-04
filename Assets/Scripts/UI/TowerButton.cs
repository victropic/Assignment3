using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButton : MonoBehaviour
{
    Tower tower = null;
    public GameObject towerPref;
    Camera cam;
    GameController gameController;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void OnClick() {
        if(tower == null) {
            Vector3 pos = cam.ScreenToWorldPoint(transform.position);
            pos = new Vector3(pos.x, pos.y, -1f);
            tower = gameController.CreateTower(pos);
        }
    }
}
