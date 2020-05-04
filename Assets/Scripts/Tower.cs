using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tower : MonoBehaviour
{

    /* Stats */
    public float range = 10f;
    public float damage = 10f;
    public float cooldownTime = 1;

    float cooldownTimer;

    /* Enemy Tracking */
    List<Enemy> enemiesInRange;

    /* Effects */
    public GameObject shootingEffectPref;

    /* Audio */
    public GameObject shootSoundPref;

    /* Sprite */
    SpriteRenderer spriteRenderer;

    /* Misc */
    System.Random random;

    void Start()
    {
        enemiesInRange = new List<Enemy>();
        random = new System.Random();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {

        SearchForEnemies();

        cooldownTimer -= Time.deltaTime;
        if(cooldownTimer <= 0f && enemiesInRange.Count > 0) {
            cooldownTimer = cooldownTime;
            
            int randIndex = Mathf.FloorToInt((float)random.NextDouble() * enemiesInRange.Count);
            Enemy chosen = enemiesInRange[randIndex];

            StartCoroutine(Attack(chosen));
        }

    }

    IEnumerator Attack(Enemy target) {
        
        
        if(target.transform.position.x > transform.position.x) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }

        GameObject shot = Instantiate(shootingEffectPref, Vector3.back * 2, Quaternion.identity);
        shot.GetComponent<ShootingEffect>().Setup(transform, target.transform);

        Instantiate(shootSoundPref);
        
        yield return new WaitForSeconds(0.5f);
        
        target.TakeDamage(damage);
    }

    public void SearchForEnemies() {
        enemiesInRange.Clear();

        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, range);
        foreach(Collider2D coll in colls) {
            if(coll.tag == "Enemy") {
                enemiesInRange.Add(coll.GetComponent<Enemy>());
            }
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = new Color(1f, 1f, 0f, 0.25f);
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
