using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeValue = 120;
    public TMP_Text timeText;
    public EmotionSpawnApproach Spawner;
    public Health PlayerHealth;
    public 

    // Update is called once per frame
    void Update()
    {
        if (timeValue > 0)
        {
        timeValue -= Time.deltaTime;
        }else{
            timeValue = 0;
            CompleteLevel();
        }
        //if (timeText.text == "01:30")
        //{
        //    Spawner.LevelStage = 2;
        //    PlayerHealth.StageUp();
        //} else if(timeText.text == "01:00")
        //{
        //    
        //}else if (timeText.text == "00:30")
        //{
        //    Spawner.LevelStage = 3;
        //}
        //I could easily replace this with an array function if I ever feel the need to reimplement it... I think it'll just be stuck in the useless library for the time being though.

        DisplayTime(timeValue);
    }

    void DisplayTime(float TimeToDisplay)
    {
        if (TimeToDisplay <0)
        {
            TimeToDisplay = 0;
        }

        float Minutes = Mathf.FloorToInt(TimeToDisplay / 60); 
            //Every minute is sixty seconds, so with each 60 seconds the value will go up one
        float Seconds = Mathf.FloorToInt(TimeToDisplay % 60);
            //Seconds can't go past a minute, so this makes it so each set of 60 above 60 will not be counted
        
            timeText.text = string.Format("{0:00}:{1:00}", Minutes, Seconds);
    }

    void CompleteLevel()
    {
        //Respawn.CompleteLevel();
    }
}
