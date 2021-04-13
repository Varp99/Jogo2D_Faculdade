using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ArenaBoss : MonoBehaviour
{
    CompositeCollider2D compositeCollider2D;
    TilemapRenderer tilemapRenderer;
    bool enteredArena = false;
    bool bossDead = false;
    GameObject boss;
    GameObject doorBoss;
    EnemyHealth enemyHealth;
    BoxCollider2D boxCollider;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        doorBoss = GameObject.Find("DoorBoss");
        compositeCollider2D = doorBoss.GetComponent<CompositeCollider2D>();
        tilemapRenderer = doorBoss.GetComponent<TilemapRenderer>();
        boss = GameObject.FindGameObjectWithTag("BossOrc");
        enemyHealth = boss.GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        if (enemyHealth.currentHealth <= 0 && !bossDead)
        {
            StartCoroutine(TimeForOpenArena());
            bossDead = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player") && !enteredArena)
        {
            boxCollider.isTrigger = true;
            StartCoroutine(TimeForCloseArena());
        }
    }

    IEnumerator TimeForCloseArena()
    {
        yield return new WaitForSeconds(0.3f);
        tilemapRenderer.enabled = true;
        compositeCollider2D.isTrigger = false;
        enteredArena = true;
        Debug.Log("Arena fechada");
    }

    IEnumerator TimeForOpenArena()
    {
        yield return new WaitForSeconds(0.5f);
        tilemapRenderer.enabled = false;
        compositeCollider2D.isTrigger = true;
        Debug.Log("Arena aberta");
    }
}
