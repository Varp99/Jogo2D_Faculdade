using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{   
    private audioController audioController;
    public string nomeCena;
    private Fade Fade;
    GameController gameController;

    void Start() {
        audioController = (audioController)FindObjectOfType(typeof(audioController));
        Fade = FindObjectOfType(typeof(Fade)) as Fade;
        Fade.fadeOut();
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
    }

    public void MudarCena()
    {
        audioController.trocarMusica(audioController.musicaFase1, nomeCena, true);
    }

    public void VoltarMenuPrincipal()
    {
        gameController.GameControllerDestroy();
        audioController.AudioControllerDestroy();
        SceneManager.LoadScene(nomeCena);
    }

    public void SairJogo()
    {
        Application.Quit();
        Debug.Log("Saiu!");
    }

    public void click()
    {
        audioController.tocarFx(audioController.fxClick, 1);
    }

    public void RestartLevel()
    {
        nomeCena = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nomeCena);
    }
}