using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : MonoBehaviour
{
    //public GameObject PauseMenu;
    public GameObject InventoryMenu;
    //public bool IsPaused = false;
    public bool IsMenu;

    void Start()
    {
        //PauseMenu.SetActive(false);
        InventoryMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) //Currently "Esc"
        {
            //if (PauseMenu.SetActive ==true)
            //    {
            //        PauseMenu.SetActive = false;
            //    } else{
            //        PauseMenu.SetActive = true;
            //    }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) //Currently "e"
        {
            if (IsMenu == false)
            {
                InventoryScreen();
            } else
            {
                InventoryResume();
            }
        }
    }
    //Pauses game for the pause screen
    //public void PauseGame()
    //{
    //    PauseMenu.SetActive(true);
    //    Time.timeScale = 0f;
    //}
    //public void ResumeGame()
    //{
    //    PauseMenu.SetActive(false);
    //    Time.timeScale = 1f;
    //}

    //Pauses game to show inventory
    public void InventoryScreen()
    {
        InventoryMenu.SetActive(true);
        IsMenu = true;
    }
    public void InventoryResume()
    {
        InventoryMenu.SetActive(false);
        IsMenu = false;
    }

    //I could probably have the pausing function work on both, but I don't want to think.
}
