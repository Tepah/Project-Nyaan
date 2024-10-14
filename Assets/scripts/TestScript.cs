using UnityEngine;

public class TestScript : MonoBehaviour
{
    public ScoreManager scoreManager;
    public void TestFunction()
    {
        Debug.Log("Button pressed! Script is working");
        scoreManager.AddScore(10);
    }
}