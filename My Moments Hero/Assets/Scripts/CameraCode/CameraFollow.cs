using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public Transform target;
    public int BXYS = 1; //This is just short for "Both, X, Y, Stationary" In which is just a general int to swap between the three modes of tracking to change how the camera moves
    //I don't like thinking how code defaults to the number zero, so I'll start the int at 1.
    private Vector3 newPos;

    // Update is called once per frame
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        newPos = new Vector3(target.position.x,transform.position.y, -10f);

    }
    void LateUpdate()
    {
        if (BXYS == 0)
            BXYS = 1; //In case a dumbass accidentally puts it at 0
        if(BXYS == 1) //default state, full movement allowed.
        {
            newPos = new Vector3(target.position.x,target.position.y, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed*Time.deltaTime);
        } else if (BXYS == 2) //X State, can only move along the X axis
        {
            newPos = new Vector3(target.position.x,transform.position.y, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed*Time.deltaTime);
        } else if (BXYS == 3) //Y State, can only move along the Y axis
        {
            newPos = new Vector3(transform.position.x,target.position.y, -10f);
            transform.position = Vector3.Slerp(transform.position,newPos, FollowSpeed*Time.deltaTime);
        } else if (BXYS == 4) //Stationary State, camera doesn't update based on the player's location anymore. But allows the camera to finish it's movement before stopping.
        {
            transform.position = Vector3.Slerp(transform.position,newPos, FollowSpeed*Time.deltaTime);
        }
    }
}
