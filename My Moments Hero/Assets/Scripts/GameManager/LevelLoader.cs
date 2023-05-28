using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            Debug.Log("Next Scene will be Scene"+ SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void LoadScene(int Scene = -1)
    {
        if (Scene == -1)
        {
            LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(Scene);
        }
    }
}
