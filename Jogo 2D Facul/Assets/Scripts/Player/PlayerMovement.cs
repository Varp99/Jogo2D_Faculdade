using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Movement Variables")]
    public float jumpForce;
    public float speed;
    public int lives;
    private bool isGrounded;
    private float movimento;
    //private bool canJump;

    //public int rings;
    //public Text TextLives;
    //public Text TextRings;

    [Header("Components")]
    private Animator animator;
    private CapsuleCollider2D capsuleCollider;
    private new Rigidbody2D rigidbody;
    private SpriteRenderer sprite;

    [Header("Attack Variables")]
    public Transform attackCheck;
    public float attackDamage = 1;
    public float timeToNextAttack;
    private float timeAttack = 0f;


    void Start()
    {
        //TextLives.text = lives.ToString();
        //TextRings.text = rings.ToString();

        capsuleCollider = GetComponent<CapsuleCollider2D>(); //Pegando o componete Collider2D
        animator = GetComponent<Animator>(); //Definindo Animator numa variavel
        rigidbody = GetComponent<Rigidbody2D>(); //Definindo Rigidbody2D numa variavel
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movimento = Input.GetAxis("Horizontal"); //Definir para andar para a esquerda ou para a direita, eixo x
        rigidbody.velocity = new Vector2(movimento * speed, rigidbody.velocity.y); //Definindo uma velocidade constante para o eixo x e para o eixo y a velocidade fixa

        //Movimentar
        if ((movimento > 0 && sprite.flipX == true) || (movimento < 0 && sprite.flipX == false))
        {
            Flip(); //Reverter a posição do eixo y, virar o player
        }

        if (movimento > 0 || movimento < 0) //Verificação se está andando
        {
            animator.SetBool("Walking", true); //Vai setar a animação booleana walking para true iniciando a animação
        }
        else
        {
            animator.SetBool("Walking", false); //Se não vai set a animação booleana walking para false parando a animação
        }

        //Pular
        if (Input.GetKey(KeyCode.Space)) //Pegar a tecla do teclado espaço para pular
        {
            if (isGrounded) //Se estiver no chão
            {
                //GetComponent<AudioSource>().Play();
                isGrounded = false;
                animator.SetBool("Jumping", true);
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
                //Debug.Log(isGrounded);
            }
            else
            {
                animator.SetBool("Jumping", false);
            }
        }

        //Atacar
        if (timeAttack <= 0f)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (movimento == 0)
                {
                    animator.SetTrigger("Attack");
                    timeAttack = timeToNextAttack;
                }
            }
        }
        else
        {
            timeAttack -= Time.deltaTime;
        }

        //Morrendo
        if (lives == 0)
        {
            //collision2D.gameObject.GetComponents<Rigidbody2D>().AddForce(new Vector2(0, 6f * Time.deltaTime), ForceMode2D.Impulse);
            //transform.position = lastCheckpoint.transform.position;
            //lives = 3;
            //TextLives.text = lives.ToString();
            //rings = 0;
            //TextRings.text = rings.ToString();

        }
    }

    //Funções
    void Flip()
    {
        sprite.flipX = !sprite.flipX;
        attackCheck.localPosition = new Vector2(-attackCheck.localPosition.x, attackCheck.localPosition.y);
    }

    private void PlayerAttack() //É chamada no evento da animação de ataque
    {
        Collider2D[] colliders = new Collider2D[3];
        transform.Find("AttackCheck").gameObject.GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), colliders);
        EnemyHealth enemyHealth;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != null && colliders[i].gameObject.CompareTag("Orc"))
            {
                enemyHealth = colliders[i].GetComponent<EnemyHealth>();
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }

    //Colisão
    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Plataformas"))
        {
            isGrounded = true;
            //GetComponent<Rigidbody2D>().gravityScale = initialGravity;
        }

        /*if (collision2D.gameObject.CompareTag("Orc"))
        {
            isGrounded = true;
            lives -= 1;
            //Debug.Log(lives);
            //TextLives.text = lives.ToString();
        }*/

        /*if (collision2D.gameObject.CompareTag("Espinhos"))
        {
            isGrounded = true;
            lives -= 1;
            //TextLives.text = lives.ToString();
        }*/

        /*if (collision2D.gameObject.CompareTag("Trampolim"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 10f);
        }

        Debug.Log("Colidiu em " + collision2D.gameObject.tag);*/
    }

    void OnCollisionExit2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Plataformas"))
        {
            isGrounded = false;
        }

        /*if (collision2D.gameObject.CompareTag("Espinhos"))
        {
            isGrounded = false;
        }*/

        //Debug.Log("Parou de colidir em " + collision2D.gameObject.tag);
    }
}