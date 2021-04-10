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

    void Awake()
    {
        compositeCollider2D = GetComponent<CompositeCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
        {
            if (playerMovement.coin == amountCoinNeed && playerMovement.key == amountKeyNeed)
            {
                Debug.Log("Vitoria vc salvou o rei");
                compositeCollider2D.isTrigger = true;
            }
            else
            {
                Debug.Log("Não possue as " + amountCoinNeed + " moedas necessárias você tem " + playerMovement.coin + " ou a chave necessaria");
            }
        }
    }
}
