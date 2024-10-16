using System.Collections.Generic;
using System.IO;
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

    private string highScoreFilePath = "Assets/highScores.json";

    void Start()
    {
        UpdateLevelText();
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        score += (int)(points * pointMultiplyer[level - 1]);
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

    [System.Serializable]
    public class HighScoreEntry
    {
        public int score;
    }

    [System.Serializable]
    public class HighScoreList
    {
        public List<HighScoreEntry> highScores = new List<HighScoreEntry>();
    }

    public void SaveHighScores(HighScoreList highScoreList)
    {
        string json = JsonUtility.ToJson(highScoreList, true);
        File.WriteAllText(highScoreFilePath, json);
    }

    public HighScoreList LoadHighScores()
    {
        if (File.Exists(highScoreFilePath))
        {
            string json = File.ReadAllText(highScoreFilePath);
            return JsonUtility.FromJson<HighScoreList>(json);
        }
        return new HighScoreList();
    }

    public void UpdateHighScores()
    {
        HighScoreList highScoreList = LoadHighScores();
        HighScoreEntry newEntry = new HighScoreEntry { score = score };

        highScoreList.highScores.Add(newEntry);
        highScoreList.highScores.Sort((x, y) => y.score.CompareTo(x.score)); 

        if (highScoreList.highScores.Count > 10)
        {
            highScoreList.highScores.RemoveAt(highScoreList.highScores.Count - 1);
        }

        SaveHighScores(highScoreList);
    }
}