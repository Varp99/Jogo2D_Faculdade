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

    public void UpdateCoinHUD(int coin)
    {
        coinTxt.text = coin + " Moedas";
    }

    public void UpdateKeyHUD(int key)
    {
        keyTxt.text = key + " Chave";
    }
}