using TMPro;
using UnityEngine;
using System.Collections;

public class HideTextAfterSeconds : MonoBehaviour
{
    public TextMeshProUGUI myText;
    public float delay = 3f;
    
    void Start()
    {
        StartCoroutine(HideTextAfterDelay());
    }

    IEnumerator HideTextAfterDelay()
    {
        yield return new WaitForSeconds(delay);  
        myText.enabled = false; 
    }
}
