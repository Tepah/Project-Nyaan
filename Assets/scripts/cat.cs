using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat : MonoBehaviour
{
    public string preferredFood;
    public AudioClip goodSound;
    public AudioClip sadSound;

    public void Feed()
    {
        if (foodType == preferredFood)
        {
            Debug.Log("Cat is happy!");
            GetComponent<AudioSource>().PlayOneShot(happySound);
                // Add more logic for happy reaction, e.g., change sprite or animation
        }
        else
        {
            Debug.Log("Cat is sad.");
            GetComponent<AudioSource>().PlayOneShot(sadSound);
                // Add more logic for sad reaction, e.g., change sprite or animation
        }
    }
}
