using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Health")]
    public float startingHealth = 3;
    public float currentHealth;
    protected bool isDead;
    private bool damaged;
    
    Animator anim;
    CapsuleCollider2D capsuleCollider;
    private SpriteRenderer sprite;
    private EnemyMovement enemyMovement;
    private audioController audioController;
    public GameObject enemy;

    protected void Awake()
    {
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        enemyMovement = GetComponent<EnemyMovement>();
        audioController = FindObjectOfType(typeof(audioController)) as audioController;
    }

    void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float amount)
    {
        if (isDead)
            return;

        damaged = true;
        StartCoroutine(DamageIndicator());
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public bool isLife()
    {
        return isDead;
    }

    IEnumerator DamageIndicator()
    {
        if (damaged)
        {
            sprite.color = Color.red;
        }
        else
        {
            damaged = false;
        }
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }

    protected void Death()
    {
        if (enemy.CompareTag("Orc"))
        {
            audioController.tocarFx(audioController.fxOrcDeath, 1);
        }
        if (enemy.CompareTag("BossOrc"))
        {
            audioController.tocarFx(audioController.fxOrcBossDeath, 1);
        }

        isDead = true;
        capsuleCollider.isTrigger = true; //Transformar a colisão do inimigo para um trigger para o player passar por cima
        anim.SetTrigger("Dead");
        enemyMovement.enabled = false;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().isKinematic = true;
        Destroy(gameObject, 5f); //Vai destruir o objeto depois de 5 segundos
    }
}