using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private string highScoreFilePath = "Assets/highScores.json";
    private HighScoreList HighScores;
    public Button levelsButton;
    public Button level2Button;
    public Button level3Button;
    public Button level4Button;
    public GameObject levelSelection;
    public GameObject settings;
    public GameObject mainMenu;
    public TextMeshProUGUI playText;
    public Slider backgroundSlider;
    public Slider itemSlider;

    public void Start()
    {
        HighScores = LoadHighScores();
        if (HighScores.highScores.Count != 0 && HighScores.highScores[0].score > 1000)
        {
            levelsButton.interactable = true;
        }

        if (PlayerPrefs.HasKey("level"))
        {
            playText.text = "Continue";
        }
    }

    public void Unlimited()
    {
        PlayerPrefs.SetInt("level", 6);
        SceneManager.LoadScene("GamePlay");
    }

    public void Play()
    {
        int level = PlayerPrefs.GetInt("level", 1);
        Debug.Log("Level: " + level);
        if (level == 5)
        {
            Debug.Log("Sending to Boss");
            SceneManager.LoadScene("GamePlayBoss");
        } else {
            SceneManager.LoadScene("GamePlay");
        }
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

        if (HighScores.highScores[0].score > 1000)
        {
            level3Button.interactable = true;
        }
        if (HighScores.highScores[0].score > 1500)
        {
            level4Button.interactable = true;
        }
    }

    public void Settings()
    {
        settings.SetActive(true);
        mainMenu.SetActive(false);
        LoadVolume();
    }

    public void ResetScores()
    {
        HighScores.highScores.Clear();
        string json = JsonUtility.ToJson(HighScores);
        File.WriteAllText(highScoreFilePath, json);
        levelsButton.interactable = false;
        level2Button.interactable = false;
        level3Button.interactable = false;
        level4Button.interactable = false;
        PlayerPrefs.DeleteKey("level");
        PlayerPrefs.DeleteKey("score");
        playText.text = "Start";
    }

    public void LoadVolume()
    {
        if (PlayerPrefs.HasKey("bgVolume"))
        {
            Debug.Log("bgVolume has key");
            float volume = PlayerPrefs.GetFloat("bgVolume");
            backgroundSlider.value = volume;
        } else {
            float volume = 0.5F;
            backgroundSlider.value = volume;
        }
        if (PlayerPrefs.HasKey("fxVolume"))
        {
            Debug.Log("fxVolume has key");
            float volume = PlayerPrefs.GetFloat("fxVolume");
            itemSlider.value = volume;
        } else {
            float volume = 0.5F;
            itemSlider.value = volume;
        }
    }

    public void onBgVolumeChange()
    {
        PlayerPrefs.SetFloat("bgVolume", backgroundSlider.value);
    }

    public void onFxVolumeChange()
    {
        PlayerPrefs.SetFloat("fxVolume", itemSlider.value);
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
        settings.SetActive(false);
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