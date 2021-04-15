using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckObjective : MonoBehaviour
{
    [SerializeField]
    int amountCoinNeed;
    [SerializeField]
    int amountKeyNeed;

    PlayerDialogo playerDialogo;
    GameController gameController;
    TransicaoCena transicaoCena;

    void Awake()
    {
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        playerDialogo = FindObjectOfType(typeof(PlayerDialogo)) as PlayerDialogo;
        transicaoCena = FindObjectOfType(typeof(TransicaoCena)) as TransicaoCena;
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
        {
            if (gameController.coin == amountCoinNeed && gameController.key == amountKeyNeed)
            {
                Debug.Log("Vitoria vc salvou o rei");
                transicaoCena.MudarCena("TelaVitoria");
            }
            else
            {
                if (gameController.coin < amountCoinNeed && gameController.key == amountKeyNeed)
                {
                    Debug.Log("Você não possue as " + amountCoinNeed + " moedas necessárias você tem " + gameController.coin);
                    playerDialogo.UpdateText("Você não possue as " + amountCoinNeed + " moedas necessárias você tem " + gameController.coin);
                }
                else if (gameController.key < amountKeyNeed && gameController.coin == amountCoinNeed)
                {
                    Debug.Log("Você não possue a chave necessária");
                    playerDialogo.UpdateText("Você não possue a chave necessária");
                }
                else
                {
                    Debug.Log("Você não possue as " + amountCoinNeed + " moedas necessárias você tem " + gameController.coin + " e você não possue a chave necessária");
                    playerDialogo.UpdateText("Você não possue as " + amountCoinNeed + " moedas necessárias você tem " + gameController.coin + " e você não possue a chave necessária");
                }
            }
        }
    }
}