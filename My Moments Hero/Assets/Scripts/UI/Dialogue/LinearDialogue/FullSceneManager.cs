using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullSceneManager : MonoBehaviour
{
    [Header("Characters")]
    List<string> CharactersSpawned = new List<string>(); //to determine if a character is already spawned
    public Sprite[] CharacterSprites; //hold all possible sprites
    private Image SpriteLocation;

    [Header("Background")]
    private Image Background; //Define where the background will be placed
    public Color newColor; //Black and invisible. (I could now use the animator's fade instead for transitional effect, this was the initial version but could be removed with some work.)
    public Color FixedColor; //Fully visible.
    [SerializeField] private Animator BackgroundFade; //Only implemented "Fade = true/false"
    [SerializeField]protected Sprite[] BackgroundList; //Just have every background in the game placed here
    
    [Header ("Items")]
    private GameObject ItemViewer; //Define where Items will show
    [SerializeField] protected Sprite[] ItemList; //Every possible Item Sprite

//This will hold responsibility over all animations, pictures, and non-interactive visuals provided in the scene

    private void Start()
    {
        Background = GameObject.Find("Background").GetComponent<Image>();
        Background.color = newColor;
        Debug.Log("FoundBackground");
        ItemViewer = GameObject.Find("ItemViewer");

        //ItemViewer.GetComponent<Image>().color = newColor;
        ItemViewer.SetActive(false);
        Debug.Log("FoundItemViewer");
        SpriteLocation = GameObject.Find("CharacterSprite").GetComponent<Image>();
    }

    //Activate Background with associated Sprite
    public void SetBackground(int Spriteground, bool Stay = false)
    {
        //Debug.Log("Setting Background now");
        if(Spriteground == 0) //Common mistake, always have a "none" background in FullSceneManager and in the cutscenes. Just something empty to fill the spot of spriteground 0.
        {
            if (Stay == false)
                StartCoroutine(BackFade(true));
            Background.color = newColor;
            return;
        }
        else
        {
            StartCoroutine(BackFade(false));
            Debug.Log("Background is " + BackgroundList[Spriteground].name);
            Background.color = FixedColor;
            Background.sprite = BackgroundList[Spriteground];
        }
    }

    //Activate ItemViewer with associated Sprite
    public void ViewItem(bool IsItemViewed, int Which = 0) //Default 0 sprite since it'd suck to have to put an int every time I use this function.
    {
        if(IsItemViewed == true)
        {
            ItemViewer.SetActive(true);
            ItemViewer.GetComponent<Image>().sprite = ItemList[Which];
        } else{
            ItemViewer.SetActive(false);
        }
    }

    //Spawn, delete and move Characters
    public void AddCharacter(string Character)
    {
        foreach(Sprite sprite in CharacterSprites)
        {
            if (Character == sprite.name)
            {
                //Choose character sprite
            }
        }
        //instanciate new Character if character was not named.
        SetCharacter(Character);
        //CharactersSpawned.Add(CharactersSpawned, Character.name);
    }
    public void SetCharacter(string Character, bool Redo = false)
    {
        Debug.Log("Character set: "+Character);
        int I = 0;
        foreach(Sprite sprite in CharacterSprites)
        {
            //Debug.Log("Character "+I+" Checked");
            if (Character == sprite.name)
            {
                SpriteLocation.sprite = CharacterSprites[I];
                return;
            }
                
            I++;
        }
        if (I == CharacterSprites.Length&&Redo == false)
        {
            SetCharacter("BLANK", true);
        }
        
            //For every character spawned, set their overlays as inactive, then set the active character's overlay as active.        
        }//Evaluates Location of character (Would set overlay and opacity of character to "Active" if there's time to add the function)
        public void EmoteCharacter(string Character)
    {
        //ChangeCharacterSprite
        SetCharacter(Character);
    }
    public void DeleteCharacter(string Character)
    {
        foreach(string Possible in CharactersSpawned)
            if (Character == Possible)
                Destroy(GameObject.Find(Character));
    }
    public void FadeBack(bool isTrue)//A bounce-between so that I could fade away during other codes
    {
        //Debug.Log("Initiating Fade");
        StartCoroutine(BackFade(isTrue));
    }
    public IEnumerator BackFade(bool isTrue)
    {
        //Debug.Log("Fade Complete");
        BackgroundFade.SetBool("Fade", isTrue);
        yield return new WaitForEndOfFrame();
    }
}
