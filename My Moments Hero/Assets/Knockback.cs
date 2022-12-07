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
            } else{
                rb.velocity = new Vector2(0,0);
                rb.AddForce(new Vector2(KnockbackForce,KnockbackForce));
            }
            KnockbackCounter -= Time.deltaTime;
        }
        
    }
    public void Knocked(int KnockPower)
    {
        KnockbackCounter += KnockPower*.1f;
    }
}
