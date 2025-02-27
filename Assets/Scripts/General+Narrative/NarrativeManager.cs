using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeManager : MonoBehaviour
{
    static int dialogIndex = 0;
    GameManager gameManager;
    DialogScriptableObj actualDialog;

    [SerializeField] DialogScriptableObj[] dialogs;
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

        switch (dialogIndex)
        {
            case 200:
                dialogIndex++;
                gameManager.GoToScene(2); // Cambio de escena en 200
                break;
            case 300:
                dialogIndex++;
                gameManager.GoToScene(3); // Cambio de escena en 300
                break;
            case 400:
                dialogIndex++;
                gameManager.GoToScene(4); // Cambio de escena en 400
                break;
            default:
                if (dialogIndex < dialogs.Length)
                {
                    actualDialog = dialogs[dialogIndex];
                    ShowDialog();
                    dialogIndex++;
                }
                break;
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
