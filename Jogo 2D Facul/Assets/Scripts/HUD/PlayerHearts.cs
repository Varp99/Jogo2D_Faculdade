using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHearts : MonoBehaviour
{
    public float currentHealth;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    private GameObject player;
    private PlayerHealth playerHealth;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        numOfHearts = (int)playerHealth.startingHealth;
    }

    private void Update()
    {
        currentHealth = playerHealth.currentHealth;
        int halfLife = (int)currentHealth;

        for (int i = 0; i < hearts.Length; i++)
        {
            /*if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }*/
            if (currentHealth > 0 && currentHealth != halfLife) //Metade da vida
            {
                hearts[i].sprite = halfHeart;
            }else if (i < currentHealth && currentHealth == halfLife)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }

            Debug.Log("i = " + i);
            Debug.Log("health = " + currentHealth);
            Debug.Log("HalfLife = " + halfLife);
            //Debug.Log("i < currentHealth && currentHealth != life = " + (i > currentHealth && currentHealth != life));
        }
    }
}
