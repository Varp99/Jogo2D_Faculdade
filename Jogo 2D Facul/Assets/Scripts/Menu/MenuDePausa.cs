using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDePausa : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenu;
    public GameObject Player;

    private void Awake()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            //Pause();
            Verificar();
            Resume();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        if (Player.activeInHierarchy == true)
        {
            Player.SetActive(false);
        }
    }

    public void Resume()
    {
        Player.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
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