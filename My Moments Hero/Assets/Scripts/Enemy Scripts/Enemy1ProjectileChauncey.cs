using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1ProjectileChauncey : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private int Damage; //this was hard coded within the file and was not usable outside this class
    public Rigidbody2D rigidBody2D;
    private Vector2 target;
    private Transform player;
    //====================================================================================

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        Vector2 moveDir = (player.transform.position-transform.position).normalized*speed;
        rigidBody2D.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(gameObject, 3);
    }

    //====================================================================================
    //Bullet recognizes that it hit something(dont use hitinfo use collision as it is more discriptive.)
    //NOTE: The code will run this script for every collider on the object. In this case it is the player
    //In this case it will run twice for the player walking collider and hitbox. Maybe I'll change it in the future, 
    //but for now, the damage will always be doubled.
    //====================================================================================

    void OnTriggerEnter2D(Collider2D collision) 
    {
        EnemyController2D enemy = collision.GetComponent<EnemyController2D>();
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
            Destroy(gameObject);
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