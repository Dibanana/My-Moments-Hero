using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyAttack : MonoBehaviour
{
    public int Damage;
    public Health PlayerHealth;
    public PlayerMovement playerMovement;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
