using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat : MonoBehaviour
{
    public string preferredFood;
    public AudioClip goodSound;
    public AudioClip touchBomb;
    public ScoreManager scoreManager;

    void Start()
    {
        touchBomb = Resources.Load<AudioClip>("Materials/touchBomb");
    }

    public void Feed(string foodType)
    {
        if (foodType == preferredFood)
        {
            Debug.Log("Cat is happy!");
            GetComponent<AudioSource>().PlayOneShot(goodSound);
                // Add more logic for happy reaction, e.g., change sprite or animation
        }
        else
        {
            Debug.Log("Cat is sad.");
            GetComponent<AudioSource>().PlayOneShot(touchBomb);
                // Add more logic for sad reaction, e.g., change sprite or animation
        }
    }
}
