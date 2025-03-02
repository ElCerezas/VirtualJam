using UnityEngine;
using UnityEngine.UI;

public class StudioController : MonoBehaviour
{
    [SerializeField]BGMusicLogic musicLogic;
    Animator animator;
    AudioSource audioSource;
    public Button load;
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
            if(PlayerPrefs.GetInt("chapterIndex") != 0 && PlayerPrefs.GetInt("dialogIndex") != 0)
            {
                load.interactable = true;
            }
            else
            {
                load.interactable= false;
            }
            gameObject.SetActive(false);
        }
    }
}
