using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckObjective : MonoBehaviour
{
    CompositeCollider2D compositeCollider2D;
    GameObject player;
    PlayerMovement playerMovement;
    [SerializeField]
    int amountCoinNeed;
    [SerializeField]
    int amountKeyNeed;

    GameController gameController;

    void Awake()
    {
        compositeCollider2D = GetComponent<CompositeCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
        {
            if (gameController.coin == amountCoinNeed && gameController.key == amountKeyNeed)
            {
                Debug.Log("Vitoria vc salvou o rei");
                compositeCollider2D.isTrigger = true;
            }
            else
            {
                Debug.Log("N�o possue as " + amountCoinNeed + " moedas necess�rias voc� tem " + gameController.coin + " ou a chave necessaria");
            }
        }
    }
}