using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //Allows you to draw upon this code from unity's system.
public class Dialogue
{
    //This code essentially hosts all information necessary to add dialogue.

    //It will work to host the essential information to be asked for each time I add dialogue.

    public string[] item; //Set up to control items and images that appear in the foreground of a cutscene

    public string[] environment; //Set up to control background+cutscenes

    public string[] name; //To keep track of who's speaking.

    [TextArea(3, 6)] //When sentences are shown in unity's bar, this changes the string area to look like a TextArea. First variable is for minimum lines allowed, while the next is for max variables allowed. (I may be guessing, but there should be a function to allow it to be limitless)
    public string[] sentences; //To keep track of what the person's saying.
}
