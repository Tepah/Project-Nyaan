using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public int level = 1;
    private double[] pointMultiplyer = new double[]{1, 1.5, 2, 2.25, 2.5, 2.5, 2.5, 2.75, 3, 3.25};
    public int goalIndex = 0;
    private int[] goals = new int[]{100, 200, 300, 400, 500, 31000, 63000, 127000, 255000, 511000, 1023000};
    public Text scoreText;
    public TextMeshProUGUI levelText;
    public GameObject[] spawners;
    public GameObject saveButtonPanel;
    public AudioSource backgroundAudio;

    public BossAppearence bossAppearence;

    private string highScoreFilePath = "Assets/highScores.json";

    void Start()
    {
        if (PlayerPrefs.HasKey("level"))
        {
            level = PlayerPrefs.GetInt("level");
            score = PlayerPrefs.GetInt("score");
            goalIndex = level - 1;
            PlayerPrefs.DeleteKey("level");
        }

        UpdateLevel();
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
            UpdateLevel();
        }
    }


    public void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    public void UpdateLevel()
    {
        levelText.text = "Level: " + level;
        
        if (level > 1)
        {
            spawners[0].SetActive(true);
        } 
        if (level > 2)
        {
            spawners[1].SetActive(true);
        } 
        if (level > 3)
        {
            spawners[2].SetActive(true);
        }
        if (level == 5 || level == 10)
        {
            saveButtonPanel.SetActive(true);
            Time.timeScale = 0;
            backgroundAudio.Pause();  
        }
    }

    public void continueBossLevel()
    {
        PlayerPrefs.SetInt("level", level);
        SceneManager.LoadScene("BossLevel");
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("score", score);
        Time.timeScale = 1;
        backgroundAudio.UnPause();
        SceneManager.LoadScene("MainMenu");
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