using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransicaoCena : MonoBehaviour
{   
    private Fade Fade;
    public string cenaDestino;
    public CapsuleCollider2D capsuleCollider;
    // Start is called before the first frame update
    
    void Awake() {
        Fade = FindObjectOfType(typeof(Fade)) as Fade;
        capsuleCollider = GetComponent<CapsuleCollider2D>(); //Pegando o componete Collider2D
    }
    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
        {
            StartCoroutine("mudancaCena");
        }
    }
    IEnumerator mudancaCena()
    {
        Fade.fadeIn();
        yield return new WaitWhile(() => Fade.fume.color.a <0.9f);
        SceneManager.LoadScene(cenaDestino);
    }
}