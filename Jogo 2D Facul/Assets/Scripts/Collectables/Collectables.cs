using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectables : MonoBehaviour
{
    private GameObject player;
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;
    private audioController audioController;
    PlayerHearts playerHearts;
    CollectablesHUD collectablesHUD;
    GameController gameController;

    void Start() 
    {
        audioController = FindObjectOfType(typeof(audioController)) as audioController;
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
    }

    private void Update()
    {
        collectablesHUD = FindObjectOfType<CollectablesHUD>();
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
                    playerHearts.AddHearts(1);
                    audioController.tocarFx(audioController.fxVida, 1);
                    Destroy(gameObject);
                }
            }

            if (gameObject.CompareTag("Coin"))
            {
                //playerMovement.coin += 1;
                gameController.coin += 1;
                collectablesHUD.UpdateCoinHUD(gameController.coin);
                audioController.tocarFx(audioController.fxMoeda, 1);
                Destroy(gameObject);
            }

            if (gameObject.CompareTag("Key"))
            {
                //playerMovement.key += 1;
                gameController.key += 1;
                collectablesHUD.UpdateKeyHUD(gameController.key);
                audioController.tocarFx(audioController.fxChave, 1);
                Destroy(gameObject);
            }
        }
    }
}