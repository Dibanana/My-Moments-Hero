using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float DuringAttackCooldown; 
    //This will keep track of current progress of cooldown.
    
    public float AttackCooldown; 
    //After the attack is complete, this will be the set time before the next attack is available.
    
    public Transform AttackPos;
    public LayerMask WhatIsEnemies;
    public float AttackRange;
    public int Damage;

    // Update is called once per frame
    void Update()
    {
        if (DuringAttackCooldown <=0){
            if (Input.GetKeyDown(KeyCode.Mouse0)){
                Collider2D[] AreaOfEffect = Physics2D.OverlapCircleAll(AttackPos.position, AttackRange, WhatIsEnemies);
                for (int i = 0; i < AreaOfEffect.Length; i++){
                    AreaOfEffect[i].GetComponent<Health>().TakeDamage(Damage);
                }
                DuringAttackCooldown = AttackCooldown;
            }
            DuringAttackCooldown = AttackCooldown;
        } else{
            DuringAttackCooldown -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPos.position, AttackRange);
    }
}
