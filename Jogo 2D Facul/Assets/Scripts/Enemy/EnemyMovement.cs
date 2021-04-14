using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Properties")]
    public float speed = 5f;
    public float climbSpeed = 1.3f;
    public int direction = 1;
    protected float playerDistance;
    public float playerDistanceMin = 2f;
    protected bool isMoving = false;
    protected bool isDead;
    private bool virou = false;
    [HideInInspector]
    public bool isRange = false;

    [Header("RayCast Properties")]
    public LayerMask layerGround;
    public float lenghtGround;
    public float lenghtWall;
    public Transform rayPointGround;
    public Transform rayPointWall;
    public RaycastHit2D hitGround;
    public RaycastHit2D hitWall;

    //protected Transform groundCheck;
    protected new Rigidbody2D rigidbody;
    protected SpriteRenderer sprite;
    protected GameObject player; //Pegar a classe player para detectar a colisão e pegar o tanto de vida do player
    protected Transform playerTransform;
    protected EnemyHealth enemyHealth;
    protected Animator anim;
    protected EnemyAttack enemyAttack;

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
        playerDistance = PlayerDistance();
        isDead = enemyHealth.isLife();

        if (isDead == false && playerDistance <= enemyAttack.distanceAttack) //Vai seguir o jogador
        {
            if (playerDistance >= playerDistanceMin)
            {
                isMoving = true;
                isRange = false;
            }
            else
            {
                isMoving = false;
                isRange = true;
            }

            if ((playerTransform.position.x > transform.position.x && virou)
                || (playerTransform.position.x < transform.position.x && !virou))
            {
                int playerPosition = (int)playerTransform.position.y;
                int enemyPosition = (int)transform.position.y;

                if (playerPosition == enemyPosition) //Se tiver na mesma altura ele pode virar
                {
                    Flip();
                }
                else //Se não tiver na mesma altura e vai continuar a patrulha
                {
                    if (!RaycastGround().collider || RaycastWall().collider)
                    {
                        Flip();
                    }
                }
            }

            if (!RaycastGround().collider)
            {
                isMoving = false;
            }
        }
        else //Vai fazer a patrulha
        {
            if (playerDistance >= playerDistanceMin)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }

            if (!RaycastGround().collider || RaycastWall().collider)
            {
                Flip();
            }
        }

        if (!isDead)
        {
            if (rigidbody.velocity.x > 0 || rigidbody.velocity.x < 0)
            {
                anim.SetBool("Walking", true);
            }
            else
            {
                anim.SetBool("Walking", false);
            }
        }

        Debug.Log(playerDistance >= playerDistanceMin);
    }

    private void FixedUpdate()
    {
        Movement();
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

    protected virtual RaycastHit2D RaycastWall()
    {
        //Send out the desired raycast and record the result
        hitWall = Physics2D.Raycast(rayPointWall.position, Vector2.right * direction, lenghtWall, layerGround);

        //Determine the color based on if the raycast hit...
        Color color = hitWall ? Color.yellow : Color.blue;
        //And draw the ray in the scene view
        Debug.DrawRay(rayPointWall.position, Vector2.right * direction * lenghtWall, color);

        //Return the results of the raycast
        return hitWall;
    }

    private void Flip()
    {
        direction *= -1;
        if (direction == 1)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            virou = false;
        }
        else if (direction == -1)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            virou = true;
        }
    }

    protected float PlayerDistance()
    {
        return Vector2.Distance(playerTransform.position, transform.position);
    }

    private void Movement()
    {
        float horizontalVelocity = speed;
        horizontalVelocity *= direction;

        if (isMoving && isDead == false)
        {
            if (rigidbody.velocity.y > 0)
            {
                rigidbody.velocity = new Vector2(horizontalVelocity * climbSpeed, rigidbody.velocity.y);
                anim.SetBool("Walking", true);
            }
            else
            {
                rigidbody.velocity = new Vector2(horizontalVelocity, rigidbody.velocity.y);
            }
        }
        else
        {
            anim.SetBool("Walking", false);
        }


        if (isDead)
        {
            rigidbody.velocity = new Vector2(0, 0);
            anim.SetBool("Walking", false);
        }
    }
}