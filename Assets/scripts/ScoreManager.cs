using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public int level = 1;
    private double[] pointMultiplyer = new double[]{1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5, 5.5};
    private int goalIndex = 0;
    private int[] goals = new int[]{1000, 3000, 7000, 15000, 31000, 63000, 127000, 255000, 511000, 1023000};
    public Text scoreText;
    public TextMeshProUGUI levelText;

    void Start()
    {
        UpdateLevelText();
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        score += points * (int)pointMultiplyer[level - 1];
        UpdateScoreText();
        if (score >= goals[goalIndex] && goalIndex < goals.Length - 1)
        {
            level++;
            goalIndex++;
            UpdateLevelText();
        }
    }

    public void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    public void UpdateLevelText()
    {
        levelText.text = "Level: " + level;
    }
}