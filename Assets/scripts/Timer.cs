using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer: MonoBehaviour
{
    public float timeRemaining = 90;
    public float totalTime = 90;
    public bool timerIsRunning = false;
    public Slider timerSlider;

    void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                timerSlider.value = timeRemaining/totalTime;
            }
            else
            {
                winGame();
            }
        }
    }

    void winGame()
    {
        Debug.Log("You win!");
        timerIsRunning = false;
        SceneManager.LoadScene("Victory");
    }

    public void resumeTimer()
    {
        timerIsRunning = true;
    }

    public void pauseTimer()
    {
        timerIsRunning = false;
    }
}