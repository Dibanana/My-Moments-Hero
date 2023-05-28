using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]private float DuringAttackCooldown; 
    //This will keep track of current progress of cooldown.
    
    public float AttackCooldown; 
    //After the attack is complete, this will be the set time before the next attack is available.
    
    public LayerMask WhatIsEnemies;
    public int Damage;
    public float Xrange;
    public float Yrange;
    public Transform AttackPos;
    public BoxCollider2D AreaOfEffect;
    [SerializeField]private int KnockPower = 1;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (DuringAttackCooldown <=0){
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Collider2D[] AreaOfEffect = Physics2D.OverlapBoxAll(AttackPos.position, new Vector2(Xrange,Yrange), 0, WhatIsEnemies);
                for (int i = 0; i < AreaOfEffect.Length; i++){
                    AreaOfEffect[i].GetComponent<Health>().TakeDamage(Damage); //For Enemies
                    AreaOfEffect[i].GetComponent<Knockback>().Knocked(KnockPower); //For Enemies
                    DuringAttackCooldown = AttackCooldown;
                }
            }
            }else{
                DuringAttackCooldown -= Time.deltaTime;
            }
//Player
    }

    //private Animator anim;


    void OnDrawGizmosSelected() //No gameplay function but visually shows a red box for what counts as range...
                                //KEEP NOTE: If you change the parameters above, try to make the same changes below.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(AttackPos.position, new Vector2(Xrange,Yrange));
    }
   //         new Vector3 (AreaOfEffect.bounds.size.x * Xrange, AreaOfEffect.bounds.size.y*Yrange, AreaOfEffect.bounds.size.z));
   // }
}
    //if (collision.gameObject.name.Equals("Player"))
    //    {
    //        playerMovement.KnockbackCounter = playerMovement.KnockbackTotalTime;
    //        if (collision.transform.position.x <= transform.position.x)
    //        {
    //            playerMovement.KnockbackRight = true;
    //        }
      //      if (collision.transform.position.x > transform.position.x)
        //    {
          //      playerMovement.KnockbackRight = false;
            //}
            //PlayerHealth.TakeDamage(Damage);
        //}