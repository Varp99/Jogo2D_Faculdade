using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectables : MonoBehaviour
{
    private GameObject player;
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
                gameController.coin += 1;
                collectablesHUD.UpdateCoinHUD(gameController.coin);
                audioController.tocarFx(audioController.fxMoeda, 1);
                Destroy(gameObject);
            }

            if (gameObject.CompareTag("BossCoin"))
            {
                gameController.coin += 1;
                collectablesHUD.UpdateCoinHUD(gameController.coin);
                audioController.tocarFx(audioController.fxMoeda, 1);
                gameController.pegouMoedaBoss = true;
                Destroy(gameObject);
            }

            if (gameObject.CompareTag("Key"))
            {
                gameController.key += 1;
                collectablesHUD.UpdateKeyHUD(gameController.key);
                audioController.tocarFx(audioController.fxChave, 1);
                Destroy(gameObject);
            }
        }
    }
}