using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class audioController : MonoBehaviour
{
    [Header("Audios Sourcers")]
    public AudioSource sMusic; // FONTE DE MUSICA
    public AudioSource sFx; // FONTE DE EFEITOS SONOROS

    [Header("Musicas")]
    public AudioClip musicaMenu;
    public AudioClip musicaFase1;

    [Header("Efeitos Sonoros")]
    public AudioClip fxClick;
    public AudioClip fxEspada;
    public AudioClip fxAndar;
    public AudioClip fxVida;
    public AudioClip fxMoeda;
    public AudioClip fxChave;
    public AudioClip fxPulo;
    public AudioClip fxDanoPlayer;
    public AudioClip fxEspadaOrc;
    public AudioClip fxOrcDeath;
    public AudioClip fxOrcBossDeath;
    public AudioClip fxOrcLaugh;
    public AudioClip fxOrcBossLaugh;

    //Configurações dos audios
    [Header("Configurações dos audios")]
    public float volumeMaximoMusica;
    public float volumeMaximoFx;

    // Configurações da troca de musica
    private AudioClip novaMusica;
    private string novaCena;
    private bool trocarCena;

    void Start()
    {
        //Faz o audio controller permanecer entre as cenas
        DontDestroyOnLoad(this.gameObject);
        // Grava os valores das musicas e efeitos
        if(PlayerPrefs.GetInt("valoresIniciais") == 0)
        {
            PlayerPrefs.SetInt("valoresIniciais", 1);
            PlayerPrefs.SetFloat("volumeMaximoMusica", 1);
            PlayerPrefs.SetFloat("volumeMaximoFx", 1);
        }
        // Carrega as configurações de audio no aparelho
        volumeMaximoMusica = PlayerPrefs.GetFloat("volumeMaximoMusica");
        volumeMaximoFx = PlayerPrefs.GetFloat("volumeMaximoFx");
        trocarMusica(musicaMenu,"MenuPrincipal",true);
    }

    public void trocarMusica(AudioClip clip, string nomeCena, bool mudarCena)
    {
        novaMusica = clip;
        novaCena = nomeCena;
        trocarCena = mudarCena;

        StartCoroutine("changeMusic");
    }

    IEnumerator changeMusic()
    {
        //Faz a diminuição do volume da musica
        for(float volume = volumeMaximoMusica; volume >=0; volume -= 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
            sMusic.volume = volume;
        }
        sMusic.volume = 0;
        sMusic.clip = novaMusica;
        sMusic.Play();

        //Aumenta o volume da música
        for(float volume = 0; volume < volumeMaximoMusica; volume += 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
            sMusic.volume = volume;
        }
        sMusic.volume = volumeMaximoMusica;

        if(trocarCena == true)
        {
            SceneManager.LoadScene(novaCena);
        }
    }

    public void tocarFx(AudioClip fx, float volume)
    {
        float tempVolume = volume;
        if(volume > volumeMaximoFx)
        {
           tempVolume = volumeMaximoFx;
        }
        sFx.volume = tempVolume;
        sFx.PlayOneShot(fx);
    }

    public void AudioControllerDestroy()
    {
        Destroy(gameObject);
    }
}