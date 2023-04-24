using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public static bool isGamePaused = false;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject _soundOptions;
    [SerializeField] GameObject _controlOptions;

    [SerializeField] AudioMixer _audioMixer;

    public void ResumeGame()
    {
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

    public void SoundOptions()
    {
        _soundOptions.SetActive(true);
    }

    public void ControlsOptions()
    {
        _controlOptions.SetActive(true);
    }

    public void ReturnFromControlsToMenu()
    {
        _controlOptions.SetActive(false);
    }

    public void SetMusicVolume(float volume)
    {
        _audioMixer.SetFloat("Music", volume);
    }

    public void SetSFXVolume(float volume)
    {
        _audioMixer.SetFloat("SFX", volume);
    }

    public void ReturnToMenu()
    {
        _soundOptions.SetActive(false);
    }

    void OnMenu()
    {
        if (!isGamePaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
              
    }
}
