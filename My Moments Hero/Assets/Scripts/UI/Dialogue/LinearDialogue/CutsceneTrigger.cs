using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] private InteractionManager Manager;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(.02f); //This is a fix for an issue where if I made an interaction before the other codes finished setting up it would break.
        Manager.Interact();
    }//Only needs to trigger dialogue as soon as scene loads
}
