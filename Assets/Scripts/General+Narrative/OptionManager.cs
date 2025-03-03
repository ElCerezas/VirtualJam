using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [SerializeField] Button bluePill, redPill;
    AudioSource audioPlayer;
    [SerializeField] AudioClip piruletasClick;
    NarrativeManager narrativeManager;
    GameManager gameManager;
    [SerializeField] GameObject character, bg, fader;
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
            DisableAll();
            bluePill.interactable = true;
            redPill.interactable = true;
            onceTime = true;
        }
        if (gameManager.gameObject.GetComponent<EventSystem>().currentSelectedGameObject == null || gameManager.gameObject.GetComponent<EventSystem>().currentSelectedGameObject == bluePill.gameObject || gameManager.gameObject.GetComponent<EventSystem>().currentSelectedGameObject == redPill||gameObject)
        {
            gameManager.gameObject.GetComponent<EventSystem>().SetSelectedGameObject(bluePill.gameObject);
        }
    }
    public void OnSelectBlue()
    {
        Debug.Log("AzulPulsado");
        fader.SetActive(false);
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
        fader.SetActive(false);
        bluePill.interactable = false;
        redPill.interactable = false;
        if (piruletasClick != null)
        {
            audioPlayer.clip = piruletasClick;
            audioPlayer.Play();
        }
        StartCoroutine(narrativeManager.FadeScreen(() => gameManager.GoToScene("Ending")));
    }

    void DisableAll()
    {
        int childs = transform.childCount;
        for (int i = 0; i < childs; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if ((child != bluePill.gameObject) && (child != redPill.gameObject) && (child != bg) && (child!=character))
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
