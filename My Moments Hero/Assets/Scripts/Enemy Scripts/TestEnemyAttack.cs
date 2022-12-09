using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyAttack : MonoBehaviour
{
    public int Damage;
    public Health PlayerHealth;
    public PlayerMovement playerMovement;
    public GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerHealth = player.GetComponent<Health>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            playerMovement.KnockbackCounter = playerMovement.KnockbackTotalTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                playerMovement.KnockbackRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                playerMovement.KnockbackRight = false;
            }
            PlayerHealth.TakeDamage(Damage);
        }
    }
}
