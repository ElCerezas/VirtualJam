using UnityEngine;

public class AudioVolumeListener : MonoBehaviour
{
    private AudioSource[] audioSources;

    private void Awake()
    {
        audioSources = GetComponents<AudioSource>();

        // Ajusta el volumen inicial al valor guardado en PlayerPrefs
        float initialVolume = PlayerPrefs.GetFloat("soundVolume", 0.5f);
        SetVolume(initialVolume);
    }

    private void OnEnable()
    {
        PauseManager.OnVolumeChange += SetVolume;
    }

    private void OnDisable()
    {
        PauseManager.OnVolumeChange -= SetVolume;
    }

    private void SetVolume(float volume)
    {
        foreach (var audioSource in audioSources)
        {
            audioSource.volume = volume;
        }
    }
}
