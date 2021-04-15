using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDePausa : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenu;
    public GameObject player;
    private string cenaAtual;
    PlayerHealth playerHealth;

    private void Awake()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Verificar();
            Resume();
        }
    }

    private void Start()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && playerHealth.currentHealth > 0)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
                Verificar();
            }
        }
    }

    void Verificar()
    {
        if (player.activeInHierarchy == true)
        {
            player.SetActive(false);
        }
    }

    public void Resume()
    {
        player.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void RestartLevel()
    {
        cenaAtual = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(cenaAtual);
    }

    void Pause()
    {
      pauseMenu.SetActive(true);
      Time.timeScale = 0f;
      GameIsPaused = true;
    }

    public void CarregaMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void FecharJogo()
    {
        Application.Quit();
        Debug.Log("Saiu!");
    }
}