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
    [SerializeField] Image background, character;
    [SerializeField] AudioSource typingSound; // Sonido de máquina de escribir
    [SerializeField] float typingSpeed = 0.05f; // Velocidad de escritura

    private Coroutine typingCoroutine;
    private bool isTyping = false;
   

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        OnNextDialog();
    }

    public void OnNextDialog()
    {
        if (isTyping) // Si aún se está escribiendo, mostrar todo de golpe
        {
            StopCoroutine(typingCoroutine);
            dialog.text = actualDialog.GetDialog();
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
        background.sprite = actualDialog.GetBackground();
        character.sprite = actualDialog.GetCharacter();
        characterName.text = actualDialog.GetName();

        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeText(actualDialog.GetDialog()));
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialog.text = "";

        foreach (char letter in text.ToCharArray())
        {
            dialog.text += letter;
            if (typingSound) typingSound.Play();
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }
}
