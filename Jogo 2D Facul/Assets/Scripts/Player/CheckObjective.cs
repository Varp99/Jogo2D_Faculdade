using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
                SceneManager.LoadScene("TelaVitoria");
            }
            else
            {
                if (gameController.coin < amountCoinNeed && gameController.key == amountKeyNeed)
                {
                    Debug.Log("Voc� n�o possue as " + amountCoinNeed + " moedas necess�rias voc� tem " + gameController.coin);
                }
                else if (gameController.key < amountKeyNeed && gameController.coin == amountCoinNeed)
                {
                    Debug.Log("Voc� n�o possue a chave necess�ria");
                }
                else
                {
                    Debug.Log("Voc� n�o possue as " + amountCoinNeed + " moedas necess�rias voc� tem " + gameController.coin + " e voc� n�o possue a chave necess�ria");
                }
            }
        }
    }
}