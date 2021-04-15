using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Movement Variables")]
    public float jumpForce;
    public float speed;
    private bool isGrounded = false;
    private float movimento;
    private bool jump = false;
    [HideInInspector]
    public bool flip = false;

    [Header("Components")]
    private Animator animator;
    [HideInInspector]
    public CapsuleCollider2D capsuleCollider;
    [HideInInspector]
    public new Rigidbody2D rigidbody;
    private SpriteRenderer sprite;
    private PlayerHealth playerHealth;
    private audioController audioController;

    [Header("Attack Variables")]
    public Transform attackCheck;
    public float attackDamage = 1;
    public float timeToNextAttack;
    private float timeAttack = 0f;

    [Header("RayCast Properties")]
    public LayerMask layerGround;
    public float lenghtGround = 2.3f;
    public Transform rayPointGround;
    public RaycastHit2D hitGround;

    void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>(); //Pegando o componete Collider2D
        animator = GetComponent<Animator>(); //Definindo Animator numa variavel
        rigidbody = GetComponent<Rigidbody2D>(); //Definindo Rigidbody2D numa variavel
        sprite = GetComponent<SpriteRenderer>();
        playerHealth = GetComponent<PlayerHealth>();
        audioController = FindObjectOfType(typeof(audioController)) as audioController;
    }

    private void Start()
    {
        if (flip)
        {
            Flip();
        }
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
            if ((Input.GetKeyDown(KeyCode.Mouse0) && movimento == 0) || (Input.GetKeyDown(KeyCode.LeftControl) && movimento == 0))
            {
                timeAttack = 0f;
                animator.SetTrigger("Attack");
            }
        }

        //Pular
        if (Input.GetKeyDown(KeyCode.Space)) //Pegar a tecla do teclado espaço para pular
        {
            Jump();
        }
        animator.SetFloat("VerticalSpeed", rigidbody.velocity.y / jumpForce);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(movimento * speed, rigidbody.velocity.y); //Definindo uma velocidade constante para o eixo x e para o eixo y a velocidade fixa
        RaycastGround();

        if (RaycastGround().collider && RaycastGround().collider != null)
        {
            animator.SetBool("IsGrounded", true);
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            animator.SetBool("IsGrounded", false);
        }

        if (jump && !isGrounded)
        {
            capsuleCollider.offset = new Vector2(capsuleCollider.offset.x, 0.9f);
            rayPointGround.localPosition = new Vector3(rayPointGround.localPosition.x, 1.99f, rayPointGround.localPosition.z);
            lenghtGround = 6f;
            StartCoroutine(JumpTime());
        }

        //Debugs
        //Debug.Log("Animator isGrounded " + animator.GetBool("IsGrounded"));
        //Debug.Log("Booleana isGrounded " + isGrounded);
        //Debug.Log("RayCast " + RaycastGround().collider);
        //Debug.Log("Movimento " + movimento);
        //Debug.Log(RaycastGround().collider);
        //Debug.Log("Jump " + jump);
        //Debug.Log(jump && isGrounded);
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
        audioController.tocarFx(audioController.fxEspada, 1);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != null && (colliders[i].gameObject.CompareTag("Orc") || colliders[i].gameObject.CompareTag("BossOrc")))
            {
                enemyHealth = colliders[i].GetComponent<EnemyHealth>();
                enemyHealth.TakeDamage(attackDamage);
            }
        }
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

    private void Jump()
    {
        if (isGrounded) //Se estiver no chão
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
            audioController.tocarFx(audioController.fxPulo, 1);
            jump = true;
        }
    }

    IEnumerator JumpTime()
    {
        yield return new WaitForSeconds(0.2f);
        if (jump && isGrounded)
        {
            capsuleCollider.offset = new Vector2(capsuleCollider.offset.x, -0.09867489f);
            rayPointGround.localPosition = new Vector3(rayPointGround.localPosition.x, 1.12f, rayPointGround.localPosition.z);
            lenghtGround = 3f;
            jump = false;
        }
    }

    //Colisão
    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Spines"))
        {
            if (playerHealth.currentHealth > 0)
            {
                playerHealth.currentHealth -= playerHealth.startingHealth;
            }
        }
    }
}