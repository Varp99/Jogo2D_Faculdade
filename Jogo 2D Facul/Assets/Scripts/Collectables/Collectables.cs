using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    private GameObject player;
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;
    private audioController audioController;
    PlayerHearts playerHearts;

    void Start() 
    {
        audioController = FindObjectOfType(typeof(audioController)) as audioController;
        playerHearts = FindObjectOfType<PlayerHearts>();
    }

    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
            playerHealth = player.GetComponent<PlayerHealth>();

            if (gameObject.CompareTag("Life"))
            {
                float life = 0f;
                life = playerHealth.startingHealth - playerHealth.currentHealth;
                if (playerHealth.currentHealth < playerHealth.startingHealth && life >= 1)
                {
                    playerHealth.currentHealth += 1;
                    //playerHearts.AddHearts(1);
                    audioController.tocarFx(audioController.fxVida, 1);
                    Destroy(gameObject);
                }
            }

            if (gameObject.CompareTag("Coin"))
            {
                playerMovement.coin += 1;
                audioController.tocarFx(audioController.fxMoeda, 1);
                Destroy(gameObject);
            }

            if (gameObject.CompareTag("Key"))
            {
                playerMovement.key += 1;
                audioController.tocarFx(audioController.fxChave, 1);
                Destroy(gameObject);
            }
        }
    }
}