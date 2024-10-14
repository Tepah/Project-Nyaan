using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cat : MonoBehaviour
{
    public string preferredFood;
    public ScoreManager scoreManager;
    public AudioClip collectFoodAudio;
    public AudioClip collectBombAudio;
    private AudioSource audioSource;

    void start()
    {

        audioSource = GetComponent<AudioSource>();

        collectFoodAudio = Resources.Load<AudioClip>("Assets/materials/gamePoint");
        collectBombAudio = Resources.Load<AudioClip>("Assets/materials/touchBomb.mp3");

        if (collectBombAudio == null && collectFoodAudio == null)
        {
            Debug.LogError("Audio Files not found! Please ensure the paths are correct");
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))  // Ensure this matches the falling item's tag
        {
            Debug.Log("Collected item!");
            scoreManager.AddScore(100);  // Add 10 points to the score
            
            if (collectFoodAudio != null && audioSource != null) //plays gamePoint.mp3
            {
                audioSource.clip = collectFoodAudio;
                audioSource.Play();
            }
            Destroy(other.gameObject);  // Destroy the falling item
        }
        else if (other.CompareTag("Bomb"))  // Ensure this matches the falling item's tag
        {
            Debug.Log("Collected bomb!");
            if (collectBombAudio != null && audioSource != null)
            {
                audioSource.clip = collectBombAudio;
                audioSource.Play();
            }
            Destroy(other.gameObject);  // Destroy the falling item
            SceneManager.LoadScene("GameOver"); 

        }
    }
}
