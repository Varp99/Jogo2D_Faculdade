using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckObjective : MonoBehaviour
{
    CompositeCollider2D compositeCollider2D;
    [SerializeField]
    int amountCoinNeed;
    [SerializeField]
    int amountKeyNeed;

    GameController gameController;

    void Awake()
    {
        compositeCollider2D = GetComponent<CompositeCollider2D>();
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
                if (gameController.coin < amountCoinNeed && gameController.key == amountKeyNeed)
                {
                    Debug.Log("Você não possue as " + amountCoinNeed + " moedas necessárias você tem " + gameController.coin);
                }
                else if (gameController.key < amountKeyNeed && gameController.coin == amountCoinNeed)
                {
                    Debug.Log("Você não possue a chave necessária");
                }
                else
                {
                    Debug.Log("Você não possue as " + amountCoinNeed + " moedas necessárias você tem " + gameController.coin + " e você não possue a chave necessária");
                }
            }
        }
    }
}