using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1ProjectileChauncey : MonoBehaviour
{

    [SerializeField] private float speed;
    public int Damage = 0; //this was hard coded within the file and was not usable outside this class - - should be public now
    public Rigidbody2D rigidBody2D;
    private Transform player;
    //====================================================================================

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 moveDir = (player.transform.position-transform.position).normalized*speed;
        rigidBody2D.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(gameObject, 3);
    }

    //====================================================================================
    //====================================================================================

    void OnTriggerEnter2D(Collider2D collision) 
    {
        EnemyShooting enemy = collision.GetComponent<EnemyShooting>();
        PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
        if (collision.CompareTag("Player"))
        {
            playerMovement.KnockbackCounter = playerMovement.KnockbackTotalTime*.5f;
            if (collision.transform.position.x <= transform.position.x)
            {
                playerMovement.KnockbackRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                playerMovement.KnockbackRight = false;
            }
            collision.GetComponent<Health>().TakeDamage(Damage);
        }
        if (enemy != null)
        {
            return;
        }
        Destroy(gameObject);
    }

    //end
}


//========================================================================================================================================================================
//========================================================================================================================================================================
//========================================================================================================================================================================
//========================================================================================================================================================================