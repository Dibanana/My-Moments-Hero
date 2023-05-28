using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
////////////////////////////////////////////////////////////////////////////////////////////////////
    //HALTING GAMEPLAY
    [SerializeField] protected bool IsGameplay = true;
    private PlayerMovement PlayerMove = null;
    private PassiveInteract PlayerATK = null;
    private PlayerController PlayerCrl = null;
    private bool InConversation = false;
////////////////////////////////////////////////////////////////////////////////////////////////////
    //RECIEVED FROM DIALOGUE
    private Queue<string> sentences; //Queue works as a (FIFO) First-in-first-out function. It is placed under the "using System.Collections;" group.
                                     //It recieves a given Array and keeps track of which order each sentence is written instead of you needing to create code to keep track of it.
                                     //For the sentences, we can set it up to load each new sentence from whatever's on the end of the queue.

    //Dialogue would only have string names listed in its assets, it's too complicated to directly add sprites and call on them with my improvised tag system. so for each name listed, the game will look for the associated character.
    private string[] DialogueName;
    private string[] EnviromentName;
    private string[] ItemNames;
////////////////////////////////////////////////////////////////////////////////////////////////////
    //TO PROVIDE THE NEXT STATE OF THE INTERACTION AFTER ENDING
    [SerializeField] private InteractionManager Interactions;
////////////////////////////////////////////////////////////////////////////////////////////////////
    //DIALOGUE ELEMENTS
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator animator;//Dialogue box's animation function NOTE: I can also make a "Shake" animation that could occur with a tag. It's cute, but just work.
/////////////////////////////////////////////////////////////////////////////////////////////////////
    [SerializeField] protected FullSceneManager SceneManager;
    [SerializeField] protected LevelLoader Level;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();

        if (IsGameplay == true)
        {
            PlayerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            PlayerATK = GameObject.FindGameObjectWithTag("Player").GetComponent<PassiveInteract>();
            PlayerCrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

        //choicesText = new TextMeshProUGUI[choices.Length];
        //int index = 0;
        //foreach (GameObject choice in choices)
        //{
        //    choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
        //    index++;
        //}

    }

    public void StartDialogue(Dialogue dialogue)
    {
        InConversation = true;
        CheckWhatsFound();
        if (Interactions.IsGameplay == true)
        {
            PlayerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            PlayerATK = GameObject.FindGameObjectWithTag("Player").GetComponent<PassiveInteract>();
            PlayerCrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
        if(PlayerMove != null)
            PlayerMove.enabled = false;
        if(PlayerATK != null)
            PlayerATK.enabled = false;
        if (PlayerCrl != null)
            PlayerCrl.enabled = false;
        
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name[0];
        sentences.Clear();
        DialogueName = dialogue.name;
        EnviromentName = dialogue.environment;
        ItemNames = dialogue.item;
        
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && InConversation == true)
        {
            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        CheckTags(sentence);
    }

    public void ChangeDisplayedName(string Name)
    {
        SceneManager.SetCharacter(Name);
        nameText.text = Name;
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        SceneManager.SetBackground(0);
        InConversation = false;
        Clear();
        animator.SetBool("IsOpen", false);
        Debug.Log("End of Conversation");
        if(PlayerMove != null)
            PlayerMove.enabled = true;
        if(PlayerATK != null)
            PlayerATK.enabled = true;
        if (PlayerCrl != null)
            PlayerCrl.enabled = true;
        if (IsGameplay == false)
        {
            //LoadNextScene(); //Have the next scene in line loaded.
        }
    }

    void CheckTags(string sentence) //Check if there are any tags to inform the conversation.
    {
        if (sentence == "_BLANK_"|| sentence =="...") //If nobody's speaking. Best if done for describing actions
        {
            SceneManager.SetCharacter("BLANK");
            nameText.text = "...";
            DisplayNextSentence();
            return;
        }
        if (sentence == "GameplayTrue") //No current use, but should theoretically prevent the player from moving
        {
            IsGameplay = true;
            DisplayNextSentence();
            return;
        }
        if (sentence == "GameplayFalse")
        {
            IsGameplay = false;
            DisplayNextSentence();
            return;
        }
        foreach (string name in DialogueName)
            if (sentence == name)
            {
                ChangeDisplayedName(sentence);
                DisplayNextSentence();
                return;
            } else if(sentence == "-"+name)//subtracts the character's presence in the scene in SceneManager (Not implemented yet)
            {
                SceneManager.DeleteCharacter(sentence);
                DisplayNextSentence();
                return;
                //SceneManager will hold an array of character and environment GameObjects and control everyone's positions, sprites, and animations.
                //When subtracting it will check if the name exists in the array, will delete the GameObject containing that name, then remove the name from the array

            } else if(sentence == "+"+name) //adds the character's presence in the scene in SceneManager
            {
                SceneManager.AddCharacter(sentence);
                DisplayNextSentence();
                return;
                //When adding, the SceneManager will check if the name exists(Will ignore repeats), will add the name to the array, then instanciate the GameObject with the name's parameters.
            }
            
        //Scrapped idea was adding emotes, movements, animations and effects to the characters. Having "face3 + name" or "anim1 + name" or something. The downside is that it adds way more checkwork and exponentially more organizing, so I'm probably gonna revamp this dialogue and scrap most of this work.
            //(BTW)God I hate this shitty dialogue setup.
            //I don't know code enough to make amy good method to viably merge variables with interactions so I just brute forced it... If anyone reads this - I'm sorry.
        
        
        int z = 0;
        foreach (string environment in EnviromentName)
        {
            if(sentence == environment)
            {
                SceneManager.SetBackground(z);
                DisplayNextSentence();
                return;
            }
            if (sentence == "+" +environment)
            {
                SceneManager.SetBackground(z, true);
                DisplayNextSentence();
                return;
            }
            z++;
        }
        int y = 0;
        foreach(string Item in ItemNames)
        {
            if (sentence == Item)
            {
                SceneManager.ViewItem(true,y);//Activate item
                DisplayNextSentence();
                return;
            }
            if (sentence == "-" + Item)
            {
                SceneManager.ViewItem(false);//Turn off item
                DisplayNextSentence();
                return;
            }
            y++;
        }

        if (sentence == "Wait")
        {
            animator.SetBool("IsOpen", false);
            StartCoroutine(Waiting());
            return;
        }
            
        if (sentence == "Continue")
        {
            EndDialogue();
            Interactions.Interact();
            //Continues to the selected dialogue.
            return;
        }
        if (sentence == "End")
        {
            EndDialogue();
            //Interactions.TimesInteracted = 0;
            Level.LoadScene();
            return;
        }
        StartCoroutine(TypeSentence(sentence)); //This only occurs if there are no tags found
    }

    public void Clear()
    {
        nameText.text = "";
        dialogueText.text = "";
    }

    private void CheckWhatsFound()
    {
        if (animator == null)
            animator = GameObject.Find("Dialogue").GetComponent<Animator>();
        if(nameText == null)
            nameText = GameObject.Find("Name").GetComponent<TextMeshProUGUI>();
        if (dialogueText == null)
            dialogueText = GameObject.Find("DialogueText").GetComponent<TextMeshProUGUI>();
        if (SceneManager == null)
            SceneManager = GameObject.Find("InteractionMaster").GetComponent<FullSceneManager>();
        //if (Interactions == null)
        //    Interactions = GameObject.Find("Mom").GetComponent<InteractionManager>(); Debug.Log("Interactions set");
    }
    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(3);
        DisplayNextSentence();
    }

}

//This will call upon the defined Queue from another Class.