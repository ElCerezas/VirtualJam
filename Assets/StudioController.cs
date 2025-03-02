using UnityEngine;

public class StudioController : MonoBehaviour
{
    [SerializeField]BGMusicLogic musicLogic;
    Animator animator;
    AudioSource audioSource;
    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            musicLogic.MusicRestart();
            gameObject.SetActive(false);
        }
    }
}
