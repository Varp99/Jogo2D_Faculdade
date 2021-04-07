using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    GameObject player;
    PlayerMovement playerMovement;
    private bool playerEnter;
    public float stairVelocity;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        if (playerEnter)
        {
            if (Input.GetKey(KeyCode.W))
            {
                playerMovement.rigidbody.velocity += new Vector2(0f, stairVelocity);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerEnter = false;
        }
    }
}
