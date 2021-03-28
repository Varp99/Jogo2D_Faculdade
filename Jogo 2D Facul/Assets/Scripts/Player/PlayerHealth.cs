using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public float startingHealth = 5f; //Começar a vida com o valor
    public float currentHealth; //Vida atual
    //public Slider healthSlider;
    //public Image damageImage;
    //public AudioClip deathClip;
    //public float flashSpeed = 5f; //Velocidade para piscar a imagem
    //public Color flashColour = new Color(1f, 0f, 0f, 0.1f); //Red, green, blue, alpha = transparencia

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    bool isDead;
    //bool damaged;

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        currentHealth = startingHealth;
    }

    void Update ()
    {
        /*if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            //Enquanto o player não levar dano vai levemente pegar a cor escolhida e depois vai limpar, ou seja, zera a cor na velocidade do flashSpeed 
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }*/
        //damaged = false;
    }

    public void TakeDamage (float amount)
    {
        //damaged = true;
        currentHealth -= amount; //Vai reduzir a vida de acordo com o dano levado
        //healthSlider.value = currentHealth; //Vai mudar o valor da barra de vida HealthUI 
        //playerAudio.Play ();

        if(currentHealth <= 0 && !isDead) //Se a vida chegar a 0 e o player ainda estiver vivo dai vai matar o player
        {
            Death ();
        }
    }

    void Death ()
    {
        isDead = true;
        anim.SetTrigger ("Dead");
        //playerAudio.clip = deathClip;
        //playerAudio.Play ();
        playerMovement.enabled = false;
    }

    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }
}
