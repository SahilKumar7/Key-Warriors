using UnityEngine;

public class VolumeChanger : MonoBehaviour
{
    private AudioSource audioSource;

    private float musicVolume = 1f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        audioSource.volume = musicVolume;
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
}