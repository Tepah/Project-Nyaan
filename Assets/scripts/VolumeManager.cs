using UnityEngine;
using UnityEngine.UI;


public class VolumeManager : MonoBehaviour
{
    public AudioSource backgroundAudio;
    public AudioSource[] itemSounds;

    void Start()
    {
        LoadVolume();
    }

    public void LoadVolume()
    {
        if (PlayerPrefs.HasKey("bgVolume"))
        {
            Debug.Log("bgVolume has key");
            float volume = PlayerPrefs.GetFloat("bgVolume");
            backgroundAudio.volume = volume;
        } 
        if (PlayerPrefs.HasKey("fxVolume"))
        {
            Debug.Log("fxVolume has key");
            float volume = PlayerPrefs.GetFloat("fxVolume");
            foreach (AudioSource itemSound in itemSounds)
            {
                itemSound.volume = volume;
            }
        }
    }
}