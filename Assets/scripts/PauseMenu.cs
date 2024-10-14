using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Pause() //pauses the game when clicked
    {   
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
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
    }

    public void Settings() //control volume
    {

    }
}
