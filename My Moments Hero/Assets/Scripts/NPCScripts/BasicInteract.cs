using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInteract : MonoBehaviour
{
    //This will be used for all non-player instances of knockback
    private Transform PlayerPos;
    private Transform ThisPos;
    private bool isFacingRight = true;
    [SerializeField]protected bool isTurnable = true;
    //[SerializeField] private DialogueTrigger trigger; //Delete later, only use this for debug purposes.
    private InteractionManager Manager;

    private void Awake()
    {
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        ThisPos = GetComponent<Transform>();
        Manager = GetComponent<InteractionManager>();
    } //Day 3 of the realization that this one line of code to autotarget the player is so fucking useful.


    public void Interaction() //The script for PlayerAttack will be responsible for this action.
    {
        if (isTurnable == true)
            CheckFlip();
        CheckTimesInteracted();
    }

    private void CheckTimesInteracted()
    {
        Debug.Log("Good Job, it works! ");
        Manager.Interact();
    }

    private void CheckFlip() //flips character to face player.
    {
        if(PlayerPos.transform.position.x >= ThisPos.transform.position.x)
            {
                if (isFacingRight == true)
                    flip();
                
            } else{
                if(isFacingRight ==false)
                    flip();
            }
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
