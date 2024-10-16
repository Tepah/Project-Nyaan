using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScoreManager : MonoBehaviour
{
    private int score;
    private string highScoreFilePath = "Assets/highScores.json";
    private HighScoreList HighScores;
    public TextMeshProUGUI[] highScoreTexts;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        score = PlayerPrefs.GetInt("score");
        scoreText.text = score.ToString();
        HighScores = LoadHighScores();
        UpdateHighScores();
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
        for (int i = 0; i < HighScores.highScores.Count; i++)
        {
            highScoreTexts[i].text = HighScores.highScores[i].score.ToString();
        }
    }
}