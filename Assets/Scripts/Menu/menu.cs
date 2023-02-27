using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public static bool isGamePaused = false;

    [SerializeField] GameObject pauseMenu;

    public void ResumeGame()
    {
        print("Hola");
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }
    
    void OnMenu()
    {
        if (!isGamePaused)
        {
            print("Pause");
            PauseGame();
        }
        else
        {
            print("Resume");
            ResumeGame();
        }
              
    }
}
