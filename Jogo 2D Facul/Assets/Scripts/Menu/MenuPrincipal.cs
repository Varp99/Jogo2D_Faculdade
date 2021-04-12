using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{   
    private audioController audioController;
    //public AudioClip fxClick;
    private float volumeMaximoFx;
    private float volume;
    public string nomeCena;
    private Fade Fade;

    void Start() {
        audioController = (audioController)FindObjectOfType(typeof(audioController));
        Fade = FindObjectOfType(typeof(Fade)) as Fade;
        Fade.fadeOut();
    }

    public void MudarCena()
    {
        audioController.trocarMusica(audioController.musicaFase1, nomeCena, true);
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
}