using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float forcaPulo;
    public float velocidadeMaxima;
    public int lives;
    //public int rings;
    //public float forcaY;
    //private float initialGravity;

    //public Text TextLives;
    //public Text TextRings;

    private bool isGrounded;
    private bool canJump;
    private bool canMove;
    //public bool inWater;
    //public bool inEscadas;
    private bool isAttack;

    //public GameObject lastCheckpoint;
    private Animator animator;

    void Start()
    {
        //Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        //TextLives.text = lives.ToString();
        //TextRings.text = rings.ToString();
        //inWater = false;
        //initialGravity = rigidbody.gravityScale; //Vai pegar a gravidade inicial do player
        

        GetComponent<CapsuleCollider2D>(); //Pegando o componete Collider2D
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>(); //Definindo Rigidbody2D numa variavel
        //Rigidbody2D fly = GetComponent<Rigidbody2D>();

        float movimento = Input.GetAxis("Horizontal"); //Definir para andar para a esquerda ou para a direita, eixo x
        //float movimento2 = Input.GetAxis("Vertical"); //Para escada
        rigidbody.velocity = new Vector2(movimento * velocidadeMaxima, rigidbody.velocity.y); //Definindo uma velocidade constante para o eixo x e para o eixo y a velocidade fixa
       
        //Movimentar
        if (movimento < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true; //Reverter a posição do eixo y, virar o player
        }else if (movimento > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (movimento > 0 || movimento < 0) //Verificação se está andando
        {
            animator.SetBool("Walking", true); //Vai setar a animação booleana walking para true iniciando a animação
        }else
        {
            animator.SetBool("Walking", false); //Se não vai set a animação booleana walking para false parando a animação
        }

        //Pular
        if (Input.GetKey(KeyCode.Space)) //Pegar a tecla do teclado espaço para pular
        {
            if (isGrounded) //Se estiver no chão
            {
                //rigidbody.AddForce(new Vector2(0, forcaPulo)); //Pegando a fisica do personagem e adicionando uma força no eixo y
                //GetComponent<AudioSource>().Play();
                canJump = true;
                isGrounded = false;
                animator.SetBool("Jumping", true);
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, forcaPulo);
                Debug.Log(isGrounded);
            }
            else
            {
                canJump = false;
                animator.SetBool("Jumping", false);
            }
        }

        //Atacar
        if (Input.GetButtonDown("Fire1"))
        {
            if (movimento == 0)
            {
                animator.SetTrigger("Attack");
                canMove = false;
            }

            //Collider2D[] colliders = new Collider2D[3];
            //transform.Find("AttackArea").gameObject.GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), colliders);
            //OverlapCollider é para detectar a colisão
            //transform.Find está procurando dentro do objeto player um objeto que se chama HammerArea 

            /*for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != null && colliders[i].gameObject.CompareTag("Monstros"))
                {
                    Destroy(colliders[i].gameObject);
                }
            }*/
        }

        

        //Debug.Log(movimento);

        /*if (!inWater) //Se ele não tiver na água pode executar o bloco
        {
            //Pular
            if (Input.GetKeyDown(KeyCode.Space)) //Pegar a tecla do teclado espaço para pular
            {
                if (isGrounded) //Se não estiver pulando
                {
                    rigidbody.AddForce(new Vector2(0, forcaPulo)); //Pegando a fisica do personagem e adicionando uma força no eixo y
                    GetComponent<AudioSource>().Play();
                    canFly = false;

                }
                else
                {
                    canFly = true;

                }
            }

            //Voando
            if (canFly && Input.GetKey(KeyCode.Space))
            {
                GetComponent<Animator>().SetBool("flying", true);
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, -0.5f);

            }
            else
            {
                GetComponent<Animator>().SetBool("flying", false);

            }

            if (isGrounded)
            {
                GetComponent<Animator>().SetBool("jumping", false);
            }
            else
            {
                GetComponent<Animator>().SetBool("jumping", true);
            }

            //Escadas
            if (inEscadas)
            {
                //gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
                //gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                rigidbody.gravityScale = 0;
                if (Input.GetKey(KeyCode.W))
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(movimento, movimento2 * velocidadeMaxima);
                    rigidbody.gravityScale = initialGravity;
                    //rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f);
                    //rigidbody.AddForce(new Vector2(0, 6f * Time.deltaTime), ForceMode2D.Impulse);
                }

                if (Input.GetKey(KeyCode.S))
                {
                    rigidbody.gravityScale = initialGravity;
                    GetComponent<Rigidbody2D>().velocity = new Vector2(movimento, movimento2 * velocidadeMaxima);
                    //rigidbody.AddForce(new Vector2(0, -6f * Time.deltaTime), ForceMode2D.Impulse);
                }
                //rigidbody.AddForce(new Vector2(0f,forcaY * Time.deltaTime), ForceMode2D.Impulse);

            }else
            {
                rigidbody.gravityScale = initialGravity; //Voltar gravidade inicial
            }

        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                rigidbody.AddForce(new Vector2(0, 6f * Time.deltaTime), ForceMode2D.Impulse);
            }

            if (Input.GetKey(KeyCode.S))
            {
                rigidbody.AddForce(new Vector2(0, -6f * Time.deltaTime), ForceMode2D.Impulse);
            }

            rigidbody.AddForce(new Vector2(0, 10f * Time.deltaTime), ForceMode2D.Impulse);

        }

        GetComponent<Animator>().SetBool("swimming", inWater);

        //Hammer
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {

            GetComponent<Animator>().SetTrigger("hammer");
            Collider2D[] colliders = new Collider2D[3];
            transform.Find("HammerArea").gameObject.GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), colliders);
            //OverlapCollider é para detectar a colisão
            //transform.Find está procurando dentro do objeto player um objeto que se chama HammerArea 

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != null && colliders[i].gameObject.CompareTag("Monstros"))
                {
                    Destroy(colliders[i].gameObject);
                }
            }


        }

    }

    //Trigger para agua
    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Water"))
        {
            inWater = true;
            Debug.Log("In " + collision2D.gameObject.tag);
            //GetComponent<Animator>().SetBool("swimming", true);
            canFly = false;
            isGrounded = false;
            GetComponent<Animator>().SetBool("jumping", false);
            GetComponent<Animator>().SetBool("walking", false);

        }

        if (collision2D.gameObject.CompareTag("Escadas"))
        {
            inEscadas = true;
            Debug.Log("In " + collision2D.gameObject.tag);
            GetComponent<Rigidbody2D>().gravityScale = initialGravity;
        }

        if (collision2D.gameObject.CompareTag("Moedas"))
        {
            rings += 1;
            Destroy(collision2D.gameObject);
            TextRings.text = rings.ToString();
        }
        /*
        if (Input.GetKeyUp(KeyCode.LeftControl) && collision2D.gameObject.CompareTag("Monstros"))
        {
            isHammering = true;
            Destroy(collision2D.gameObject);
        }
        */
        /*if (collision2D.gameObject.CompareTag("Checkpoint"))
        {
            lastCheckpoint = collision2D.gameObject;
        }*/

    }

    /*private void OnTriggerExit2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Water"))
        {
            inWater = false;
            Debug.Log("Out " + collision2D.gameObject.tag);
            //GetComponent<Animator>().SetBool("swimming", false);
            GetComponent<Animator>().SetBool("jumping", true);
            //canFly = true;
        }

        if (collision2D.gameObject.CompareTag("Escadas"))
        {
            inEscadas = false;
            Debug.Log("In " + collision2D.gameObject.tag);
            
        }
    }*/

    //Colisão
    void OnCollisionEnter2D(Collision2D collision2D)
    {
       

        if (collision2D.gameObject.CompareTag("Plataformas"))
        {
            isGrounded = true;
            //GetComponent<Rigidbody2D>().gravityScale = initialGravity;
        }

        /*if (collision2D.gameObject.CompareTag("Monstros"))
        {
            isGrounded = true;
            lives -= 1;
            TextLives.text = lives.ToString();
        }

        if (collision2D.gameObject.CompareTag("Espinhos"))
        {
            isGrounded = true;
            lives -= 1;
            TextLives.text = lives.ToString();
        }

        if (collision2D.gameObject.CompareTag("Trampolim"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 10f);
        }

        Debug.Log("Colidiu em " + collision2D.gameObject.tag);

        //Morrendo
        if (lives == 0)
        {
            //collision2D.gameObject.GetComponents<Rigidbody2D>().AddForce(new Vector2(0, 6f * Time.deltaTime), ForceMode2D.Impulse);
            transform.position = lastCheckpoint.transform.position;
            lives = 3;
            TextLives.text = lives.ToString();
            rings = 0;
            TextRings.text = rings.ToString();

        }*/

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
