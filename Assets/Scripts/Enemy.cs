﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject path;
    public EnemySpawner enemySpawner;

    /* Stats */
    public float maxHealth = 100f;
    public int killReward = 5;
    public float damageToCastle = 10f;
    public float speed = 2f;

    public float health;

    /* States */
    bool alive = true;

    /* Navigation */
    GameObject targetWayPoint;
    int targetChildIndex = 0;

    /* Sprite */
    SpriteRenderer spriteRenderer;
    Animator animator;

    /* Audio */
    public GameObject hitSoundPref;

    /* Misc */
    GameController gameController;

    void Start()
    {
        // Setup Stats
        health = maxHealth;

        targetWayPoint = path.transform.GetChild(0).gameObject;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        if(alive) {
            Vector3 toTarget = (targetWayPoint.transform.position - transform.position);
            toTarget = new Vector3(toTarget.x, toTarget.y, 0f);

            if(toTarget.magnitude > 0.1f) {
                Vector3 path = toTarget.normalized * speed * Time.deltaTime;

                animator.SetFloat("speedX", path.x);
                animator.SetFloat("speedY", path.y);
                transform.position += path;
            } else {
                if(targetChildIndex < path.transform.childCount - 1) {
                    targetChildIndex++;
                    targetWayPoint = path.transform.GetChild(targetChildIndex).gameObject;
                } else {
                    alive = false;
                    StartCoroutine(Die());
                    gameController.CastleTakeDamage(damageToCastle);
                }
            }
        }
        
    }

    public void TakeDamage(float amount) {
        if(alive) {
            health -= amount;

            Instantiate(hitSoundPref);

            if(health <= 0) {
                alive = false;
                StartCoroutine(Die());
                gameController.CastleCollectGold(killReward);
            } else {
                StartCoroutine(ReactToHit());
            }
        }
    }

    IEnumerator ReactToHit() {
        spriteRenderer.color = Color.red;
        
        yield return new WaitForSeconds(0.1f);

        spriteRenderer.color = Color.white;
    }
    
    IEnumerator Die() {
        enemySpawner.numDied++;

        float t = 0f;
        for(int i = 0; i < 20f; i++) {
            t = ((float)i)/20f;
            spriteRenderer.color = Color.Lerp(Color.white, new Color(0f, 0f, 0f, 0f), t);
            yield return new WaitForSeconds(0.02f);
        }
        

        Destroy(gameObject);
    }
}
