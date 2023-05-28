using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //Will only begin dialogue, doesn't continue it. Meaning this will act as the external initiator for the conversation before it starts up.

    //The code to continue a dialogue will be elsewhere.
    [SerializeField] protected string ConversationTitle;
    
    public Dialogue dialogue;
    
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
