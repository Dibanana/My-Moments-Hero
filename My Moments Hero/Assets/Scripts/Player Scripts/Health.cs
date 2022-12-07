using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float health;
    public bool HealthInfinite;
    //public bool IsDead = false;
    // Update is called once per frame
    void Update()
    {
        if (HealthInfinite == true)
        {
            return;
        }else{
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public void TakeDamage(int Damage)
    {
        health -= Damage;
        //Damage will be taken
    }
    private void Death()
    {
        //Remember to later get the animation set to the enemy so that we can call on the death animation here.
        //Destroy(gameObject);
    }
}
