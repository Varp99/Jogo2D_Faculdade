using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float startingHealth = 3;
    public float currentHealth;
    public float sinkSpeed = 2.5f; //Velocidade do corpo sumir
    //public AudioClip deathClip;

    Animator anim;
    //AudioSource enemyAudio;
    //ParticleSystem hitParticles;
    CapsuleCollider2D capsuleCollider;
    protected bool isDead;
    protected bool isSinking;


    protected void Awake()
    {
        anim = GetComponent<Animator>();
        //enemyAudio = GetComponent<AudioSource>();
        //hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        currentHealth = startingHealth;
    }


    void Update()
    {
        if (isSinking)
        {
            //Afundar o corpo multiplicando pela velocidade e pelo tempo da ultima execução
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage(float amount)
    {
        if (isDead)
            return;

        //enemyAudio.Play();
        //hitParticles.transform.position = hitPoint; //Vai pegar o local onde o tiro foi acertado
        //hitParticles.Play();

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Death();
        }
        //Debug.Log(currentHealth);
    }

    protected void Death()
    {
        isDead = true;
        capsuleCollider.isTrigger = true; //Transformar a colisão do inimigo para um trigger para o player passar por cima
        anim.SetTrigger("Dead");
        //enemyAudio.clip = deathClip;
        //enemyAudio.Play();
        StartSinking();
    }

    public void StartSinking()
    {
        //Quando esse inimigo morrer não vai mais fazer parte da navegação IA
        //GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        //Quando esse inimigo morrer os outros inimigos vão ignorar o corpo e permitir passar por aquele caminho no caso calcular aquele local para passar
        GetComponent<Rigidbody2D>().isKinematic = true;
        isSinking = true;
        Destroy(gameObject, 5f); //Vai destruir o objeto depois de 2 segundos
    }
}
