using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransicaoCena : MonoBehaviour
{   
    private Fade Fade;
    public string cenaDestino = "Mapa_1";

    GameObject player;
    GameController gameController;
    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Awake() {
        Fade = FindObjectOfType(typeof(Fade)) as Fade;
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        playerMovement = player.GetComponent<PlayerMovement>();

        if (gameController.passouMapa)
        {
            if (cenaDestino == "Mapa_2") //Do mapa 1 para o mapa 2
            {
                player.transform.position = new Vector3(200.43f, -50.61f, player.transform.position.z);
                playerMovement.flip = true;
                gameController.passouMapa = false;
            }

            if (cenaDestino == "Mapa_1") //Do mapa 2 para o mapa 1
            {
                player.transform.position = new Vector3(-43.33f, 9.5f, player.transform.position.z);
                gameController.passouMapa = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
        {
            StartCoroutine("mudancaCena");
        }
    }

    public void MudarCena(string cena)
    {
        cenaDestino = cena;
        StartCoroutine("mudancaCena");
    }

    IEnumerator mudancaCena()
    {
        Fade.fadeIn();
        yield return new WaitWhile(() => Fade.fume.color.a <0.9f);
        SceneManager.LoadScene(cenaDestino);
        gameController.passouMapa = true;
    }
}