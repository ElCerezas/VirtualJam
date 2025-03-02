using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource SoundEffects;

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
        }
    }

    public void PlaySFX(string sfxName)
    {
        switch (sfxName)
        {
            case "Coin":
                SoundEffects.PlayOneShot(CoinSound);
                break;
            case "BreakBlock":
                SoundEffects.PlayOneShot(BreakBlock);
                break;
            case "Damage":
                SoundEffects.PlayOneShot(EnemyDamage);
                break;
            case "Jump":
                SoundEffects.PlayOneShot(Jump);
                break;
            default:
                break;
        }
    }
}
