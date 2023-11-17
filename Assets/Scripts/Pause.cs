using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    public bool GameIsPaused;
    public GameObject pauseMenuUI;
    public bool IsMenuOverwritten;

    private void Start()
    {
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
    }

    public void Update()
    {
       if (GameIsPaused == false)
       {
           Time.timeScale = 1f;
           pauseMenuUI.SetActive(false);
       }
       
       if(GameIsPaused == true)
       {
           Time.timeScale = 0f;
           pauseMenuUI.SetActive(true);
       }

        if (Input.GetKeyDown(KeyCode.Escape) && IsMenuOverwritten == false)
        {
            GameIsPaused = !GameIsPaused;
        }
    }

    public void ResumeGame()
    {
        GameIsPaused = !GameIsPaused;
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

