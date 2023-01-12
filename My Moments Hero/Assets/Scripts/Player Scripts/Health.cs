using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private HealthUIScript HealthUI;
    public int health;
    public int MaxHealth;
    public bool HealthInfinite;
    public bool DamageIsTaken;
    [SerializeField] private bool IsPlayer = false;
    //public bool IsDead = false;
    // Update is called once per frame
    void Start()
    {
        health = MaxHealth;
    }
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
            if(IsPlayer == true)
                HealthUI.UpdateHealth();
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
        //Make sure all death animations have the same animator names to make sure I don't need to repeat this process for every enemy.
        //Destroy(gameObject);
    }
    IEnumerator DamageWait() //prevent damage from affecting the instance multiple times in one frame (a serious problem with projectile attacks)
    {
        yield return new WaitForEndOfFrame();
        DamageIsTaken = false;
    }
}
