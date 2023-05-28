using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool Paused = false;
    [SerializeField] protected GameObject PauseUI;

    public void SetVolume (float volume)
    {
        Debug.Log(volume);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            if (Paused == false)
            {
                Pause();
            } else{
                Unpause();
            }
        }
    }
    public void Pause()
    {
        Time.timeScale = 0;
        PauseUI.SetActive(true);
        Paused = true;
    }
    public void Unpause()
    {
        Time.timeScale = 1;
        PauseUI.SetActive(false);
        Paused = false;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
