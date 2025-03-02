using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    private AudioSource _backgroundMusic;
    private AudioSource _soundEffects;

    [Header("Audio Clips")]
    public AudioClip CoinSound;
    public AudioClip BreakBlock;
    public AudioClip EnemyDamage;
    public AudioClip Jump;
    public AudioClip PowerUp0;
    public AudioClip PowerUp1;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            _backgroundMusic = new AudioSource();
            _soundEffects = new AudioSource();
        }
        
    }

    public void PlaySFX(string sfxName)
    {
        switch (sfxName)
        {
            case "Coin":
                _soundEffects.PlayOneShot(Jump);
                break;
            case "BreakBlock":
                _soundEffects.PlayOneShot(Jump);
                break;
            case "EnemyDamage":
                _soundEffects.PlayOneShot(Jump);
                break;
            case "Jump":
                _soundEffects.PlayOneShot(Jump);
                break;
            default:
                _soundEffects.PlayOneShot(Jump);
                break;
        }
    }
}
