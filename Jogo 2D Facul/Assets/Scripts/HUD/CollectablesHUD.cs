using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectablesHUD : MonoBehaviour
{
    [SerializeField]
    Text coinTxt;
    [SerializeField]
    Text keyTxt;

    GameController gameController;

    private void Awake()
    {
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        UpdateCoinHUD(gameController.coin);
        UpdateKeyHUD(gameController.key);
    }

    public void UpdateCoinHUD(int coin)
    {
        coinTxt.text = coin + " Moedas";
    }

    public void UpdateKeyHUD(int key)
    {
        keyTxt.text = key + " Chave";
    }
}