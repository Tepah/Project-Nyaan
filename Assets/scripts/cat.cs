using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cat : MonoBehaviour
{
    public string preferredFood;
    public AudioSource itemSound;
    public ScoreManager scoreManager;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            Debug.Log("Collected item!");
            scoreManager.AddScore(100);  
            itemSound.Play();
            Destroy(other.gameObject);  // Destroy the falling item
        }
        else if (other.CompareTag("Bomb"))
        {
            Debug.Log("Collected bomb!");
            Destroy(other.gameObject);  // Destroy the falling item
            PlayerPrefs.SetInt("score", scoreManager.score);
            scoreManager.UpdateHighScores();
            SceneManager.LoadScene("GameOver"); 
        }    
    }
    
}
