using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour 
{    
    public DialogueTrigger[] interactions;
    public bool IsGameplay = true;
    
    public int TimesInteracted; //Keep track of how many times the player talks to this NPC. Likely to vary the responses
    private int LastDialogue;
    [SerializeField] private int RandomInteractions = 3; //So I could just set how many random interactions there are

    //[SerializeField] private int Choices = 0; //In case there are any choices in the dialogue to be mentioned. Place choices at very end. I have absolutely no idea how to add choices.

    public void Interact()
    {
        if (TimesInteracted == 0)
        {
            InitialInteraction();
            TimesInteracted++;
        }else{
            int UniqueDialogues = interactions.Length - RandomInteractions;// -Choices; Subtract choices if adding the function
            if (TimesInteracted > UniqueDialogues)
            {
                RandomGenericDialogue(Random.Range(0,RandomInteractions));
            } else{
                NextUniqueDialogue(TimesInteracted);
                TimesInteracted++;
            }
        }        
    }
    private void InitialInteraction() //This does the same exact function as NextUniqueDialogue at the moment, but I'll keep it separate in case I want to customize the sequence.
    {
        interactions[0].TriggerDialogue();
        //Debug.Log("Times Interacted: "+TimesInteracted);
    }


    private void RandomGenericDialogue(int RandomizedDialogue)
    {
        if (RandomizedDialogue != LastDialogue)
        {
            Debug.Log("Interaction Chosen: "+(TimesInteracted+RandomizedDialogue));
            interactions[TimesInteracted+RandomizedDialogue-1].TriggerDialogue();
            //Once provided the random number from it's prior code, it will take the corresponding dialogue from this area.
            LastDialogue = RandomizedDialogue;
        } else
        {
            RandomGenericDialogue(Random.Range(0,2));
        }
    }

    //Make sure to repeat this process for as long as there are interactions. We can then Add a variable to allow for any code that goes unused.
    //There's also gonna be a space to write the dialogue in Unity so don't think you'll need to write the script with the script.
    private void NextUniqueDialogue(int TimesInteracted)
    {
        Debug.Log("Times Interacted: "+TimesInteracted);
        interactions[TimesInteracted].TriggerDialogue();

    }
}
