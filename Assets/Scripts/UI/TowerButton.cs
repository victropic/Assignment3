using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButton : MonoBehaviour
{
    Tower tower = null;
    public GameObject towerPref;
    Camera cam;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void OnClick() {
        if(tower == null) {
            Vector3 pos = cam.ScreenToWorldPoint(transform.position);
            pos = new Vector3(pos.x, pos.y, -1);
            tower = Instantiate(towerPref, pos, Quaternion.identity).GetComponent<Tower>();
        }
    }
}
