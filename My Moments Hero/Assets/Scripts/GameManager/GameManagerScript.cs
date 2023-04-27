using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private bool UsePlatformEffector = false;
    private GameObject[] platforms;
    // Start is called before the first frame update
    void Start()
    {
        if(UsePlatformEffector == true)
        {
            platforms = GameObject.FindGameObjectsWithTag("Platform"); //Automatically sets up every object under the plaform tag to have a platform effector upon starting the game.
                                                                       //Doing so would totally backfire if I actually wanted to cut corners like that, but it's a good testing ground to try out adding components to others.
            foreach (GameObject item in platforms)
            {
                item.GetComponent<PlatformEffector2D>();
            }
        }
    }
}
