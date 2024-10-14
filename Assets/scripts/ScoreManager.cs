using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    public Text scoreText;

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int points) //add some stuff for sound
    {
        score += points;
        UpdateScoreText();
        //playsound function();
    }

    public void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
}