using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [SerializeField] Button bluePill, redPill;
    AudioSource audioPlayer;
    [SerializeField] AudioClip piruletasClick;
    NarrativeManager narrativeManager;
    GameManager gameManager;
    bool onceTime = false;
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        audioPlayer = GetComponent<AudioSource>();
        narrativeManager = GetComponent<NarrativeManager>();
    }
    public void OptionSelectionStart()
    {
        if (!onceTime)
        {
            bluePill.interactable = true;
            redPill.interactable = true;
            onceTime = true;
        }
    }
    public void OnSelectBlue()
    {
        Debug.Log("AzulPulsado");
        bluePill.interactable = false;
        redPill.interactable = false;
        if (piruletasClick != null)
        {
            audioPlayer.clip = piruletasClick;
            audioPlayer.Play();
        }
        StartCoroutine(narrativeManager.FadeScreen(() => gameManager.GoToScene("GameOver")));
    }
    public void OnSelectRed()
    {
        Debug.Log("RojoPulsado");
        bluePill.interactable = false;
        redPill.interactable = false;
        if (piruletasClick != null)
        {
            audioPlayer.clip = piruletasClick;
            audioPlayer.Play();
        }
        StartCoroutine(narrativeManager.FadeScreen(() => gameManager.GoToScene("Ending")));
    }
}
