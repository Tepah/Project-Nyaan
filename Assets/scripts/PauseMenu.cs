using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] Slider backgroundSlider;
    [SerializeField] Slider itemSlider;
    public AudioSource backgroundAudio;
    public AudioSource itemSound;
    public Timer timer;

    public void Pause() //pauses the game when clicked
    {   
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        backgroundAudio.Pause();  
        timer.pauseTimer();
    }

    public void Home() //quits the game and goes to main menu screen
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void Resume() //resumes the game as it was
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        backgroundAudio.UnPause();
        timer.resumeTimer();
    }

    public void Settings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
        LoadVolume();
    }

    public void LoadVolume()
    {
        if (PlayerPrefs.HasKey("bgVolume"))
        {
            Debug.Log("bgVolume has key in Pause Menu");
            float volume = PlayerPrefs.GetFloat("bgVolume");
            backgroundSlider.value = volume;
        } else {
            float volume = backgroundAudio.volume;
            backgroundSlider.value = volume;
        }
        if (PlayerPrefs.HasKey("fxVolume"))
        {
            Debug.Log("fxVolume has key in Pause Menu");
            float volume = PlayerPrefs.GetFloat("fxVolume");
            itemSlider.value = volume;
        } else {
            float volume = itemSound.volume;
            itemSlider.value = volume;
        }
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("bgVolume", backgroundSlider.value);
        PlayerPrefs.SetFloat("fxVolume", itemSound.volume);
    }

    public void AdjustItemVolume(float volume)
    {
        itemSound.volume = volume;
    }

    public void CloseSettings() 
    {
        backgroundAudio.volume = backgroundSlider.value;
        itemSound.volume = itemSlider.value;
        SaveVolume();
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
