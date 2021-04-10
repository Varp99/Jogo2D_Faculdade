using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public float startingHealth = 5f; //Começar a vida com o valor
    public float currentHealth; //Vida atual
    //public Slider healthSlider;
    private SpriteRenderer sprite;
    private audioController audioController;
    private string cenaAtual;

    Animator anim;
    PlayerMovement playerMovement;
    //PlayerHearts playerHearts;
    bool isDead;
    bool damaged;

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerMovement = GetComponent <PlayerMovement> ();
        currentHealth = startingHealth;
        sprite = GetComponent<SpriteRenderer>();
        audioController = FindObjectOfType(typeof(audioController)) as audioController;
        //playerHearts = FindObjectOfType<PlayerHearts>();
        PlayerHearts.instance.SetupHearts((int)startingHealth);
    }

    void Update ()
    {
        if (currentHealth <= 0 && !isDead) //Se a vida chegar a 0 e o player ainda estiver vivo dai vai matar o player
        {
            Death();
        }
    }

    public void TakeDamage (float amount)
    {
        damaged = true;
        StartCoroutine(DamageIndicator());
        currentHealth -= amount; //Vai reduzir a vida de acordo com o dano levado
        PlayerHearts.instance.RemoveHearts(amount);

        if(currentHealth <= 0 && !isDead) //Se a vida chegar a 0 e o player ainda estiver vivo dai vai matar o player
        {
            Death ();
        }
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

    void Death ()
    {
        playerMovement.capsuleCollider.offset = new Vector2(playerMovement.capsuleCollider.offset.x, -0.09867489f);
        audioController.tocarFx(audioController.fxDanoPlayer, 1);
        isDead = true;
        anim.SetTrigger ("Dead");
        PlayerHearts.instance.SetCurrentHealth(0f);
        playerMovement.enabled = false;
        StartCoroutine(AutoRestartLevel());
    }

    IEnumerator AutoRestartLevel()
    {
        yield return new WaitForSeconds(2f);
        RestartLevel();
    }

    public void RestartLevel ()
    {
       cenaAtual = SceneManager.GetActiveScene().name;
       SceneManager.LoadScene (cenaAtual);
    }
}