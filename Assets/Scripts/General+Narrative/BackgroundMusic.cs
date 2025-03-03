using UnityEngine;

public class BGMusicLogic : MonoBehaviour
{
    public static BGMusicLogic Instance { get; private set; }
    private AudioSource bgMusic;
    [SerializeField] AudioClip[] musicas;
    GameManager gameManager;

    int sceneIndex;

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
        gameManager = GetComponent<GameManager>();
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
    private void Update()
    {
        if(sceneIndex != gameManager.GetSceneIndex())
        {
            sceneIndex = gameManager.GetSceneIndex();
            switch (sceneIndex)
            {
                case 2: //Boxer
                    bgMusic.clip = musicas[1];
                    bgMusic.Play();
                    break;
                case 3:
                    bgMusic.clip = musicas[2];
                    bgMusic.Play();
                    break;
                case 4:
                    bgMusic.clip = musicas[3];
                    bgMusic.Play();
                    break;
                case 7:
                    bgMusic.clip = musicas[4];
                    bgMusic.Play();
                    break;
                default:
                    if (bgMusic.clip == musicas[0])
                    {
                        break;
                    }
                    bgMusic.clip = musicas[0];
                    bgMusic.Play();
                    break;
            }
        }
    }
}
