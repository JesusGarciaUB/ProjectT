using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;

public class menustart : MonoBehaviour
{
    public int scenenum;
    [SerializeField] AudioMixer myMixer;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume") && myMixer != null)
        {
            LoadVolume();
        }
    }

    private void LoadVolume()
    {
        float cosa1 = PlayerPrefs.GetFloat("musicVolume");
        float cosa2 = PlayerPrefs.GetFloat("SFXVolume");
        myMixer.SetFloat("Music", Mathf.Log10(cosa1) * 20);
        myMixer.SetFloat("SFX", Mathf.Log10(cosa2) * 20);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(scenenum);
    }
    public void doExitGame()
    {
        Application.Quit();
    }


}
