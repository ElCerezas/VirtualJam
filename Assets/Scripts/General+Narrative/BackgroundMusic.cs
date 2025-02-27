using UnityEngine;

public class BGMusicLogic : MonoBehaviour
{
    public static BGMusicLogic Instance { get; private set; }
    private AudioSource bgMusic;

    private void Awake()
    {
        //Singleton setUp
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        //End of Singleton


        bgMusic = GetComponent<AudioSource>();
        bgMusic.playOnAwake = true;
    }
    public void MusicResumeStop()
    {
        if (!bgMusic.isPlaying)
        {
            bgMusic.Pause();
        }
        else
        {
            bgMusic.UnPause();
        }
    }
    public void MusicRestart()
    {
        bgMusic.Stop();
        bgMusic.Play();
    }
}
