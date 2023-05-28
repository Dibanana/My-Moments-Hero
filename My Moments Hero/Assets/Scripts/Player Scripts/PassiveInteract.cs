using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveInteract : MonoBehaviour
{
    public LayerMask WhatIsNPC;
    public float Xrange;
    public float Yrange;
    public Transform Pos;
    public BoxCollider2D AreaOfEffect;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Collider2D[] AreaOfEffect = Physics2D.OverlapBoxAll(Pos.position, new Vector2(Xrange, Yrange), 0, WhatIsNPC);
            for (int i = 0; i < AreaOfEffect.Length; i++)
            {
                AreaOfEffect[i].GetComponent<BasicInteract>().Interaction();
            }
        }
    }

    //private Animator anim;


    void OnDrawGizmosSelected() //No gameplay function but visually shows a red box for what counts as range...
                                //KEEP NOTE: If you change the parameters above, try to make the same changes below.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Pos.position, new Vector2(Xrange, Yrange));
    }
}
