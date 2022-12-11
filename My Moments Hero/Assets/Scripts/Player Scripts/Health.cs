using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float health;
    public bool HealthInfinite;
    public bool DamageIsTaken;
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
        if (DamageIsTaken == true){
            StartCoroutine(DamageWait());
        }
    }
    public void TakeDamage(int Damage)
    {
        if(DamageIsTaken==false){
            health -= Damage;
            DamageIsTaken = true;}
        //Damage will be taken
    }
    private void Death()
    {
        //Remember to later get the animation set to the enemy so that we can call on the death animation here.
        //Destroy(gameObject);
    }
    IEnumerator DamageWait() //prevent damage from affecting the instance multiple times in one frame (a serious problem with projectile attacks)
    {
        yield return new WaitForEndOfFrame();
        DamageIsTaken = false;
    }
}
