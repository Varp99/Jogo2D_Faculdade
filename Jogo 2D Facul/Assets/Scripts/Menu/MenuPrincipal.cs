using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{   
   
    private audioController audioController;
    public AudioClip fxClick;
    private float volumeMaximoFx;
    private float volume;

    void Start() {
        audioController = (audioController)FindObjectOfType(typeof(audioController));
    }
    public void MudarCena()
    {
        string nomeCena = "Mapa_2";
        
        audioController.trocarMusica(audioController.musicaFase1, nomeCena, true);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
