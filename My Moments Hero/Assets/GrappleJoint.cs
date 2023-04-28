using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class GrappleJoint : MonoBehaviour
{
    [SerializeField] private Grappler grapple;
    public int Damage;
    public int KnockPower;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Health>().TakeDamage(Damage);
            collision.GetComponent<Knockback>().Knocked(KnockPower);
            if(grapple.isSwinging == true)
                grapple.EndSwing(true, .5f);
            if(grapple.isGrappling == true)
                grapple.EndGrapple(true, .5f);
        }
        else
        {
            //grapple.GrappleExtended = true;
            //I want this part to end the grapple extention early so that the grapple could hit walls, but I'm just gonna sideline that for now.
            //Trying to learn how to use RaycastHit2D fucked up too many times that I find it borderline unusable and I'm out of ideas for executing collision with walls.
            //So in the end, I don't know how to do it.
        }
    }
}
