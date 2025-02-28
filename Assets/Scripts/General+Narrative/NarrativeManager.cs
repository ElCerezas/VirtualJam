using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeManager : MonoBehaviour
{
    public static int dialogIndex = 0;
    public static int chapterIndex = 0;
    GameManager gameManager;

    DialogScriptableObj actualDialog;
    DialogScriptableObj[] actualScene;

    [SerializeField] DialogScriptableObj[] Chapter1, Chapter2, Chapter3;
    [SerializeField] TMP_Text dialog, characterName;
    [SerializeField] Image background, character, textImage;
    [SerializeField] Image fader; //Imagen negra para transición
    [SerializeField] AudioSource typingSound;

    private Coroutine typingCoroutine;
    private bool isTyping = false;

    [SerializeField] private float fadeDuration = 0.5f; // Duración del fade

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        switch (chapterIndex)
        {
            case 0: actualScene = Chapter1; break;
            case 1: actualScene = Chapter2; break;
            case 2: actualScene = Chapter3; break;
        }
        OnNextDialog();
    }

    public void OnNextDialog()
    {
        if (actualDialog == null)
        {
            actualDialog = actualScene[dialogIndex];
        }
        if (isTyping) // Si aún se está escribiendo, mostrar todo de golpe
        {
            StopCoroutine(typingCoroutine);
            dialog.text = actualDialog.GetDialog();
            if (typingSound) typingSound.Stop();
            isTyping = false;
            return;
        }
        if (actualDialog.fadeOut)
        {
            StartCoroutine(FadeScreen(() => WhatNext()));
            return;
        }
        WhatNext();
    }

    void ShowDialog()
    {
        if (textImage.enabled) textImage.sprite = actualDialog?.TextImage;
        if (character.enabled) character.sprite = actualDialog?.Character;
        if (background.enabled) background.sprite = actualDialog?.Background;
        typingSound.clip = actualDialog.audio;
        typingSound.loop = actualDialog.loopAudio;
        characterName.text = actualDialog.GetName();

        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeText(actualDialog.GetDialog()));
    }
    void WhatNext()
    {
        if (dialogIndex < actualScene.Length)
        {
            actualDialog = actualScene[dialogIndex];
            textImage.enabled = actualDialog.GetDialog() == null || actualDialog.GetDialog() == "" ? false : true;
            character.enabled = actualDialog.Character == null ? false : true;
            background.enabled = actualDialog.Background == null ? false : true;

            ShowDialog();
            dialogIndex++;
        }
        else
        {
            dialogIndex = 0;
            chapterIndex++;
            switch (chapterIndex)
            {
                case 1: gameManager.GoToScene(2); break;
                case 2: gameManager.GoToScene(3); break;
                case 3: gameManager.GoToScene(4); break;
                default:
                    gameManager.GoToScene(5); // Ending
                    break;
            }
        }
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
    IEnumerator FadeScreen(System.Action onComplete)
    {
        float elapsedTime = 0f;
        Color startColor = fader.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f); // Opaco

        // Fade Out (Oscurece la pantalla)
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fader.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            yield return null;
        }

        // Ejecutar la acción (cambiar de diálogo)
        onComplete?.Invoke();

        elapsedTime = 0f;
        startColor = fader.color;
        endColor = new Color(startColor.r, startColor.g, startColor.b, 0f); // Transparente

        // Fade In (Aparece el nuevo diálogo)
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fader.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            yield return null;
        }
    }
}
