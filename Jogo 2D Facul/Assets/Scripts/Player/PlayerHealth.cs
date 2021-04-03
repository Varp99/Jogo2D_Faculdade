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
    //public AudioClip deathClip;
    //public float flashSpeed = 5f; //Velocidade para piscar a imagem
    //public Color flashColour = new Color(1f, 0f, 0f, 0.1f); //Red, green, blue, alpha = transparencia

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    bool isDead;
    bool damaged;

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        currentHealth = startingHealth;
        sprite = GetComponent<SpriteRenderer>();
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
        //healthSlider.value = currentHealth; //Vai mudar o valor da barra de vida HealthUI 
        //playerAudio.Play ();

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
        isDead = true;
        anim.SetTrigger ("Dead");
        //playerAudio.clip = deathClip;
        //playerAudio.Play ();
        playerMovement.enabled = false;
        StartCoroutine(AutoRestartLevel());
    }

    IEnumerator AutoRestartLevel()
    {
        yield return new WaitForSeconds(5f);
        RestartLevel();
    }

    public void RestartLevel ()
    {
        SceneManager.LoadScene (1);
    }
}
