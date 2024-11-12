using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string highScoreFilePath = "Assets/highScores.json";
    private HighScoreList HighScores;
    public Button levelsButton;
    public Button level2Button;
    public Button level3Button;
    public Button level4Button;
    public GameObject levelSelection;
    public GameObject mainMenu;

    public void Start()
    {
        HighScores = LoadHighScores();
        if (HighScores.highScores.Count != 0 && HighScores.highScores[0].score > 1000)
        {
            levelsButton.interactable = true;
        }

    }

    public void Play()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void Settings()
    {
        SceneManager.LoadScene("settings");
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player Has Quit the game");
    }
    
    public void Levels()
    {
        levelSelection.SetActive(true);
        mainMenu.SetActive(false);

        if (HighScores.highScores[0].score > 3000)
        {
            level3Button.interactable = true;
        }
        if (HighScores.highScores[0].score > 7000)
        {
            level4Button.interactable = true;
        }
    }

    public void onClickLevel2() 
    {
        PlayerPrefs.SetInt("level", 2);
        SceneManager.LoadScene("GamePlay");
    }

    public void onClickLevel3()
    {
        PlayerPrefs.SetInt("level", 3);
        SceneManager.LoadScene("GamePlay");
    }

    public void onClickLevel4()
    {
        PlayerPrefs.SetInt("level", 4);
        SceneManager.LoadScene("GamePlay");
    }

    public void Back()
    {
        levelSelection.SetActive(false);
        mainMenu.SetActive(true);
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

    public HighScoreList LoadHighScores()
    {
        if (File.Exists(highScoreFilePath))
        {
            string json = File.ReadAllText(highScoreFilePath);
            return JsonUtility.FromJson<HighScoreList>(json);
        }
        return new HighScoreList();
    }
}