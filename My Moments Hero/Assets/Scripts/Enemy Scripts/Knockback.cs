using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    //This will be used for all non-player instances of knockback
    public float KnockbackForce;
    public float KnockbackCounter;
    public Transform PlayerPos;
    public int KnockPower;
    [SerializeField]private Transform ThisPos;
    [SerializeField] private Rigidbody2D rb;
    private bool isFacingRight = true;

    void Awake()
    {
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
    } //Day 3 of the realization that this one line of code to autotarget the player is so fucking useful.
    private void FixedUpdate()
    {
        if(KnockbackCounter <= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        } else
        {
            if(PlayerPos.transform.position.x >= ThisPos.transform.position.x)
            {
                rb.velocity = new Vector2(0,0);
                rb.AddForce(new Vector2(-KnockbackForce,KnockbackForce));
                if (isFacingRight == true)
                    flip();
            } else{
                rb.velocity = new Vector2(0,0);
                rb.AddForce(new Vector2(KnockbackForce,KnockbackForce));
                if(isFacingRight ==false)
                    flip();
            }
            KnockbackCounter -= Time.deltaTime;
        }
        
    }
    public void Knocked(int KnockPower)
    {
        KnockbackCounter += KnockPower*.1f;
    }
    private void flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    //For future, include a stun or wait timer when attacked from behind to prevent player from getting ganked
    //reference to enemyshooting and set float variable "EnemyShooting.attackCooldown" to a negative number.
}
