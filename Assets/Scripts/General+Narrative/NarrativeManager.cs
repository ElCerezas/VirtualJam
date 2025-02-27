using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeManager : MonoBehaviour
{
    static int dialogIndex = 0;
    static int chapterIndex = 0;
    GameManager gameManager;

    DialogScriptableObj actualDialog;
    DialogScriptableObj[] actualScene;

    [SerializeField] DialogScriptableObj[] Chapter1, Chapter2, Chapter3;
    [SerializeField] TMP_Text dialog, characterName;
    [SerializeField] Image background, character, textImage;
    [SerializeField] AudioSource typingSound; // Sonido de máquina de escribir
    [SerializeField] float typingSpeed = 0.05f; // Velocidad de escritura

    private Coroutine typingCoroutine;
    private bool isTyping = false;
   

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        switch (chapterIndex)
        {
            case 0:
                actualScene = Chapter1; break;
            case 1:
                actualScene = Chapter2; break;
            case 2:
                actualScene = Chapter3; break;
        }
        OnNextDialog();
    }

    public void OnNextDialog()
    {
        if (isTyping) // Si aún se está escribiendo, mostrar todo de golpe
        {
            StopCoroutine(typingCoroutine);
            dialog.text = actualDialog.GetDialog();
            if (typingSound) typingSound.Stop();
            isTyping = false;
            return;
        }
        if (dialogIndex < actualScene.Length)
        {
            actualDialog = actualScene[dialogIndex];
            ShowDialog();
            dialogIndex++;
        }
        else
        {
            dialogIndex = 0;
            chapterIndex++;
            switch (chapterIndex)
            {
                case 1:
                    gameManager.GoToScene(2);
                    break;
                case 2:
                    gameManager.GoToScene(3);
                    break;
                case 3:
                    gameManager.GoToScene(4);
                    break;
                default:
                    gameManager.GoToScene(5); //Ending
                    break;
            }
        }
    }

    void ShowDialog()
    {
        /*textImage.sprite = actualDialog?.TextImage;
        background.sprite = actualDialog?.Background;
        character.sprite = actualDialog?.Character;*/
        characterName.text = actualDialog.GetName();

        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeText(actualDialog.GetDialog()));
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialog.text = "";

        if (typingSound) typingSound.Play();
        foreach (char letter in text.ToCharArray())
        {
            dialog.text += letter;
            yield return new WaitForSeconds(actualDialog.writtingSpeed);
        }
        if (typingSound) typingSound.Stop();
        isTyping = false;
    }
}
