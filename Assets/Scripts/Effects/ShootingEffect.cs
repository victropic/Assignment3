using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEffect : MonoBehaviour
{

    public float timeToLive = 0.1f;
    
    Transform origin = null;
    Transform target = null;

    float timer;
    
    void Start()
    {
        timer = timeToLive;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f) {
            Destroy(gameObject);
        }
 
        if(origin != null && target != null) {
            transform.position = Vector3.Lerp(target.position, origin.position, timer/timeToLive);
            transform.position = new Vector3(transform.position.x, transform.position.y, -2f);
            transform.rotation = GetRotation();
        }
    }

    Quaternion GetRotation() {

        float angleFromEast = Vector3.Angle(Vector3.right, origin.position - target.position);
        float angleFromNorth = Vector3.Angle(Vector3.up, origin.position - target.position);

        float angle = angleFromEast;
        if(angleFromNorth < 90f) {
            angle *= -1f;
        }

        return Quaternion.AngleAxis(angle, Vector3.back);
    }

    public void Setup(Transform origin, Transform target) {
        this.origin = origin;
        this.target = target;
    }
}
