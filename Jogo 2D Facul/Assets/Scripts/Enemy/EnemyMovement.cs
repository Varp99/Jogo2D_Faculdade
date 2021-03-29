using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;
    protected float playerDistance;
    public float playerDistanceMin = 2f;
    //protected float movement = 1f;
    //bool isRight = true;
    protected bool isMoving = false;
    protected bool isDead;

    //protected Transform groundCheck;
    protected new Rigidbody2D rigidbody;
    protected SpriteRenderer sprite;
    protected GameObject player; //Pegar a classe player para detectar a colisão e pegar o tanto de vida do player
    protected Transform playerTransform;
    protected EnemyHealth enemyHealth;
    protected Animator anim;
    private EnemyAttack enemyAttack;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        anim = GetComponent <Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyAttack = GetComponent<EnemyAttack>();
    }

    void Update()
    {
        /*transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D ground = Physics2D.Raycast(groundCheck.position, Vector2.down, distance);

        if (ground.collider == false)
        {
            if (isRight)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                sprite.flipX = true;
                Turn();
                isRight = false;
                Debug.Log("Esquerda");
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                sprite.flipX = false;
                Turn();
                isRight = true;
                Debug.Log("Direita");
            }
        }*/

        playerDistance = PlayerDistance();
        //isMoving = playerDistance <= enemyAttack.distanceAttack;
        isDead = enemyHealth.isLife();

        if (isDead == false && playerDistance <= enemyAttack.distanceAttack)
        {
            if (playerDistance >= playerDistanceMin)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }

            if ((playerTransform.position.x > transform.position.x && sprite.flipX)
                || (playerTransform.position.x < transform.position.x && !sprite.flipX))
            {
                Flip();
            }
        }

        if (rigidbody.velocity.x > 0 || rigidbody.velocity.x < 0)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }

        //Debug.Log(Vector2.down); 
        //Debug.DrawRay(groundCheck.position, groundCheck.TransformDirection(Vector3.forward) * distance, Color.yellow);
    }

    private void FixedUpdate()
    {
        if (isMoving && isDead == false)
        {
            rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
        }else if (isDead)
        {
            rigidbody.velocity = new Vector2(0, 0);
        }
        //Debug.Log(PlayerInRange());
        //Debug.Log(playerDistance);
        //Debug.Log(playerDistanceMin);
        //Debug.Log(playerInRange);
        //Debug.Log(playerTransform.position.x - transform.position.x);
        //PlayerInRange();
    }

    private void Flip()
    {
        sprite.flipX = !sprite.flipX;
        speed *= -1;
        enemyAttack.attackCheck.localPosition = new Vector2(-enemyAttack.attackCheck.localPosition.x, enemyAttack.attackCheck.localPosition.y);
    }

    protected float PlayerDistance()
    {
        return Vector2.Distance(playerTransform.position, transform.position);
    }
}