using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cat : MonoBehaviour
{
    public string preferredFood;
    public AudioSource bombSound;
    public AudioSource itemSound;
    public ScoreManager scoreManager;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))  // Ensure this matches the falling item's tag
        {
            Debug.Log("Collected item!");
            scoreManager.AddScore(100);  // Add 10 points to the score
            Destroy(other.gameObject);  // Destroy the falling item
        }
        else if (other.CompareTag("Bomb"))  // Ensure this matches the falling item's tag
        {
            Debug.Log("Collected bomb!");
            Destroy(other.gameObject);  // Destroy the falling item
            SceneManager.LoadScene("GameOver"); 
        }
    }
}
