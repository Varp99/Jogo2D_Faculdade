using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Player Variables")]
    public float startingHealth = 5f; //Começar a vida com o valor
    public float currentHealth; //Vida atual
    public int coin = 0;
    public int key = 0;
    string cenaAtual;

    GameObject player;
    PlayerHealth playerHealth;
    GameObject collectables_1;
    GameObject collectables_2;

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        collectables_1 = GameObject.Find("Collectables_1");
        collectables_2 = GameObject.Find("Collectables_2");
        collectables_1.SetActive(false);
        collectables_2.SetActive(false);
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cenaAtual = SceneManager.GetActiveScene().name;

        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
            currentHealth = playerHealth.currentHealth;
        }

        if (collectables_1 != null)
        {
            if (cenaAtual == "Mapa_1")
            {
                collectables_1.SetActive(true);
                collectables_2.SetActive(false);
            }

            if (cenaAtual == "Mapa_2")
            {
                collectables_2.SetActive(true);
                collectables_1.SetActive(false);
            }
        }
    }
}