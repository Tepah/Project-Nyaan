using UnityEngine;

public class LevelController : MonoBehaviour
{
    private int level;
    public ScoreManager scoreManager;
    public GameObject[] spawners;

    void Start()
    {
        if (PlayerPrefs.HasKey("level"))
        {
            level = PlayerPrefs.GetInt("level");
            PlayerPrefs.DeleteKey("level");
        } else {
            level = 1;
        }
    }

    private void manageSpawners() 
    {
        if (level == 2)
        {
            spawners[0].SetActive(true);
        } else if (level == 3)
        {
            spawners[1].SetActive(true);
        } else if (level > 4)
        {
            spawners[2].SetActive(true);
        }
    }
}