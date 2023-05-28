using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Respawning : MonoBehaviour
{
    public bool isGameOver;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void CompleteLevel()
    {
            isGameOver = true;
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // gets the active scene, we dont have to remember scene names
    }
}
