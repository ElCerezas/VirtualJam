using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public delegate void SoundsVolume(float volume);
    public static event SoundsVolume OnVolumeChange;

    GameManager gameManager;

    [SerializeField] private AudioSource musicSoundSource;
    [SerializeField] private Slider musicSlider, VFXSlider;
    [SerializeField] private GameObject pauseScreen;

    public static float VFXvolume;
    private bool isPaused = false;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
        musicSoundSource.volume = PlayerPrefs.GetFloat("musicVolume", 0.2f);
        musicSlider.value = musicSoundSource.volume;
        VFXvolume = PlayerPrefs.GetFloat("soundVolume", 0.5f);
        VFXSlider.value = VFXvolume;

        ResumeGame(); // Asegura que el juego empiece despausado
    }

    void Update()
    {
        // Detecta la tecla Escape en teclado o el botón Start en un mando
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
        {
            Debug.Log("PauseButton");
            OnPause();
        }
    }

    // Ajustes de volúmenes:
    public void NewSoundVolume()
    {
        VFXvolume = VFXSlider.value;
        PlayerPrefs.SetFloat("soundVolume", VFXvolume);
        OnVolumeChange?.Invoke(VFXvolume);
    }

    public void MusicChange()
    {
        musicSoundSource.volume = musicSlider.value;
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }

    // Pausa
    public void OnPause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        pauseScreen.SetActive(true);
        Time.timeScale = 0f; // Pausa el juego
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void ResumeGame()
    {
        isPaused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1f; // Reanuda el juego

        if (gameManager.GetSceneIndex() == 3 || gameManager.GetSceneIndex() == 4 || gameManager.GetSceneIndex() == 5)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
