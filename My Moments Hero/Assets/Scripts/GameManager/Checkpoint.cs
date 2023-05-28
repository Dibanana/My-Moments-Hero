using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameManagerScript gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManagerScript>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gm.lastCheckPointPos=transform;
        }
    }
    
}
