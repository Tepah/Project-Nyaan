using UnityEngine;

public class ForceResolution : MonoBehaviour
{
    void Start()
    {
        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
    }

    void Update()
    {
        if (Screen.width != 1920 || Screen.height != 1080)
        {
            Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
        }
    }
}