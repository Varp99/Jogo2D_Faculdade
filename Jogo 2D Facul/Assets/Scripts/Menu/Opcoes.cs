using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opcoes : MonoBehaviour
{
    private audioController audioController;
    public Slider volumeMusica;
    public Slider volumeFX;
    
    void Start()
    {   
        audioController = FindObjectOfType(typeof(audioController)) as audioController;
        volumeMusica.value = audioController.volumeMaximoMusica;
        volumeFX.value = audioController.volumeMaximoFx;
    }
    public void alterarVolumeMusica()
    {
        float tempVolumeMusica = volumeMusica.value;
        audioController.volumeMaximoMusica = tempVolumeMusica;
        audioController.sMusic.volume = tempVolumeMusica;
        PlayerPrefs.SetFloat("volumeMaximoMusica", tempVolumeMusica);
    }
    public void alterarVolumeFX()
    {
        float tempVolumeFX = volumeFX.value;
        audioController.volumeMaximoFx = tempVolumeFX;
        PlayerPrefs.SetFloat("volumeMaximoFx", tempVolumeFX);
    }
}
