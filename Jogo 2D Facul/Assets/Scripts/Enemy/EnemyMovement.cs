using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : EnemyAttack
{
    public float speed = 5f;
    protected float playerDistance = 1f;
    public float playerDistanceMin = 1f;
    protected float movement = 1f;
    //bool isRight = true;
    protected bool isMoving = false;

    //protected Transform groundCheck;
    protected new Rigidbody2D rigidbody;
    protected SpriteRenderer sprite;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        anim = GetComponent <Animator>();
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
        isMoving = (playerDistance <= distanceAttack);

        if (isMoving)
        {
            if (playerDistance >= playerDistanceMin)
            {
                isMoving = true;
            }
            else
            {
                Attack();
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
        if (isMoving)
        {
            rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
        }
        //Debug.Log(playerDistance >= playerDistanceMin);
    }

    private void Flip()
    {
        sprite.flipX = !sprite.flipX;
        speed *= -1;
    }

    protected float PlayerDistance()
    {
        return Vector2.Distance(playerTransform.position, transform.position);
    }
}
