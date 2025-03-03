using UnityEngine;

public class TennisSoundManager : MonoBehaviour
{
    public static TennisSoundManager Audio { get; private set; }

    [Header("Audio Sources")]
    private AudioSource _soundEffects;

    [Header("Audio Clips")]
    public AudioClip PunchSound;
    public AudioClip PartPassSound;
    public AudioClip FailSound;

    [SerializeField]
    private CameraShake _camera;

    void Start()
    {
        if (Audio == null)
        {
            Audio = this;
            _soundEffects = GetComponent<AudioSource>();
        }

    }

    private void OnEnable()
    {
        Movement.OnHitSound += OnHitSound;
        EnemyMovement.OnHitSound += OnHitSound;

        GameCounter.OnFailSound += OnFailSound;
        GameCounter.OnPartPassSound += OnPartPassSound;
    }

    private void OnDisable()
    {
        Movement.OnHitSound -= OnHitSound;
        EnemyMovement.OnHitSound -= OnHitSound;

        GameCounter.OnFailSound -= OnFailSound;
        GameCounter.OnPartPassSound -= OnPartPassSound;
    }

    private void OnHitSound()
    {
        _camera.StartShake();
        _soundEffects.PlayOneShot(PunchSound);
    }
    
    private void OnFailSound()
    {
        _soundEffects.PlayOneShot(FailSound);
    }

    private void OnPartPassSound()
    {
        _soundEffects.PlayOneShot(PartPassSound);
    }
}
