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
    public int coin = 0;
    public int key = 0;
    private bool isGrounded;
    private float movimento;

    //public int rings;
    //public Text TextLives;
    //public Text TextRings;

    [Header("Components")]
    private Animator animator;
    private CapsuleCollider2D capsuleCollider;
    private new Rigidbody2D rigidbody;
    private SpriteRenderer sprite;
    private PlayerHealth playerHealth;

    [Header("Attack Variables")]
    public Transform attackCheck;
    public float attackDamage = 1;
    public float timeToNextAttack;
    private float timeAttack = 0f;

    [Header("RayCast Properties")]
    public LayerMask layerGround;
    public float lenghtGround;
    public Transform rayPointGround;
    public RaycastHit2D hitGround;

    [Header("AudioCLips")]
    public AudioClip playerAttack;
    public AudioSource audioSource;

    void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>(); //Pegando o componete Collider2D
        animator = GetComponent<Animator>(); //Definindo Animator numa variavel
        rigidbody = GetComponent<Rigidbody2D>(); //Definindo Rigidbody2D numa variavel
        sprite = GetComponent<SpriteRenderer>();
        playerHealth = GetComponent<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
        playerAttack = GetComponent<AudioClip>();
    }

    void Update() 
    {
        movimento = Input.GetAxis("Horizontal"); //Definir para andar para a esquerda ou para a direita, eixo x

        //Movimentar
        if ((movimento > 0 && sprite.flipX == true) || (movimento < 0 && sprite.flipX == false))
        {
            Flip(); //Reverter a posição do eixo y, virar o player
        }

        if (movimento != 0) //Verificação se está andando
        {
            animator.SetBool("Walking", true); //Vai setar a animação booleana walking para true iniciando a animação
        }
        else
        {
            animator.SetBool("Walking", false); //Se não vai set a animação booleana walking para false parando a animação
        }

        //Atacar
        timeAttack += Time.deltaTime;
        if (timeAttack >= timeToNextAttack)
        {
            //if (Input.GetButtonDown("Fire1") && movimento == 0)
            if ((Input.GetKeyDown(KeyCode.Mouse0) && movimento == 0) || (Input.GetKeyDown(KeyCode.LeftControl) && movimento == 0))
            {
                timeAttack = 0f;
                animator.SetTrigger("Attack");
            }
        }

        if (RaycastGround().collider && RaycastGround().collider != null)
        {
            animator.SetBool("IsGrounded", true);
            isGrounded = true;
            animator.SetBool("Jumping", false);
        }
        else
        {
            isGrounded = false;
            animator.SetBool("IsGrounded", false);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(movimento * speed, rigidbody.velocity.y); //Definindo uma velocidade constante para o eixo x e para o eixo y a velocidade fixa
        RaycastGround();

        //Pular
        if (Input.GetKeyDown(KeyCode.Space)) //Pegar a tecla do teclado espaço para pular
        {
            if (isGrounded)
            animator.SetTrigger("Jumping");
            //Jump();
        }

        //Debugs
        Debug.Log("Animator isGrounded " + animator.GetBool("IsGrounded"));
        Debug.Log("Animator Jumping " + animator.GetBool("Jumping"));
        Debug.Log("Booleana isGrounded " + isGrounded);
        Debug.Log("RayCast " + RaycastGround().collider);
        //Debug.Log("Movimento " + movimento);
        //Debug.Log(RaycastGround().collider);
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
        PlaySound(playerAttack);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != null && colliders[i].gameObject.CompareTag("Orc"))
            {
                enemyHealth = colliders[i].GetComponent<EnemyHealth>();
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    protected virtual RaycastHit2D RaycastGround()
    {
        //Send out the desired raycast and record the result
        hitGround = Physics2D.Raycast(rayPointGround.position, Vector2.down, lenghtGround, layerGround);

        //Determine the color based on if the raycast hit...
        Color color = hitGround ? Color.red : Color.green;
        //And draw the ray in the scene view
        Debug.DrawRay(rayPointGround.position, Vector2.down * lenghtGround, color);

        //Return the results of the raycast
        return hitGround;
    }

    private void Jump() //É chamada no evento da animação de pulo
    {
        //if (isGrounded) //Se estiver no chão
        //{
            //GetComponent<AudioSource>().Play();
            isGrounded = false;
            //animator.SetBool("Jumping", true);
            animator.SetBool("IsGrounded", false);
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
            //Debug.Log(isGrounded);
        //}
    }

    //Colisão
    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Plataformas"))
        {
            //isGrounded = true;
            //animator.SetBool("Jumping", false);
            //animator.SetBool("IsGrounded", true);
            //GetComponent<Rigidbody2D>().gravityScale = initialGravity;
        }

        if (collision2D.gameObject.CompareTag("Spines"))
        {
            if (playerHealth.currentHealth > 0)
            {
                playerHealth.currentHealth -= playerHealth.startingHealth;
            }
            //TextLives.text = lives.ToString();
        }

        /*if (collision2D.gameObject.CompareTag("Trampolim"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 10f);
        }*/
    }

    void OnCollisionExit2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Plataformas"))
        {
            //isGrounded = false;
            //animator.SetBool("IsGrounded", false);
        }
    }
}