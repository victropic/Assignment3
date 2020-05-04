using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastingObject : MonoBehaviour
{
    public float timeToLive = 1f;

    void Start()
    {
        StartCoroutine(Die());
    }

    IEnumerator Die() {

        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }
}
