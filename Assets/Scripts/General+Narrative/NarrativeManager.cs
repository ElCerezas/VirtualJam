using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NarrativeManager : MonoBehaviour
{
    public static int dialogIndex = 0;
    public static int chapterIndex = 0;
    GameManager gameManager;
    OptionManager optionManager;

    DialogScriptableObj actualDialog;
    DialogScriptableObj[] actualScene;

    [SerializeField] DialogScriptableObj[] Chapter1, Chapter2, Chapter3, Chapter4;
    [SerializeField] TMP_Text dialog, characterName;
    [SerializeField] Image background, character, textImage;
    [SerializeField] Image fader;
    [SerializeField] AudioSource typingSound;
    [SerializeField] AudioClip typing;

    private Coroutine typingCoroutine;
    private bool isTyping = false;

    [SerializeField] private float fadeDuration = 0.5f;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        optionManager = GetComponent<OptionManager>();
        Debug.Log("GettingInfo");

        chapterIndex = PlayerPrefs.GetInt("chapterIndex");
        switch (chapterIndex)
        {
            case 0: actualScene = Chapter1; break;
            case 1: actualScene = Chapter2; break;
            case 2: actualScene = Chapter3; break;
            case 3: actualScene = Chapter4; break;
        }
        if (PlayerPrefs.GetInt("dialogIndex") != 0) {dialogIndex = PlayerPrefs.GetInt("dialogIndex") - 2; } else { dialogIndex = 0; }
        Debug.LogWarning($"Dialog: {dialogIndex} chapter: {chapterIndex}");
        OnNextDialog();
    }

    public void OnNextDialog()
    {
        if (actualDialog == null)
        {
            actualDialog = actualScene[dialogIndex];
        }
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialog.text = actualDialog.GetDialog();
            isTyping = false;

            if (typingSound && typingSound.loop)
            {
                typingSound.Stop();
            }
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
        characterName.text = actualDialog.GetName();

        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeText(actualDialog.GetDialog()));
    }

    void WhatNext()
    {
        if (dialogIndex < actualScene.Length)
        {
            typingSound.clip = actualDialog.audio;
            typingSound.loop = false;
            typingSound.Stop();
            typingSound.Play();
            actualDialog = actualScene[dialogIndex];
            textImage.enabled = actualDialog.GetDialog() == null || actualDialog.GetDialog() == "" ? false : true;
            character.enabled = actualDialog.Character == null ? false : true;
            background.enabled = actualDialog.Background == null ? false : true;

            ShowDialog();
            dialogIndex++;
        }
        else
        {
            switch (chapterIndex)
            {
                case 0: gameManager.NarrativeGoToGame(2); break; //Boxer
                case 1: gameManager.NarrativeGoToGame(3); break; //Tenis
                case 2: gameManager.NarrativeGoToGame(4); break; //Wario
                default:
                    //Time to decide
                    GetComponent<PlayerInput>().enabled = false;
                    dialog.text = "";
                    characterName.text = "";
                    textImage.enabled = false;
                    optionManager.OptionSelectionStart();
                    break;
            }
        }
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialog.text = "";

        foreach (char letter in text.ToCharArray())
        {
            if (!typingSound.isPlaying)
            {
                typingSound.clip = typing;
                typingSound.loop = true;
                typingSound.Play();
            }
            dialog.text += letter;
            yield return new WaitForSeconds(actualDialog.writtingSpeed);
        }
        typingSound.Stop();
        isTyping = false;
    }

    public IEnumerator FadeScreen(System.Action onComplete)
    {
        float elapsedTime = 0f;
        Color startColor = fader.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fader.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            yield return null;
        }

        onComplete?.Invoke();

        elapsedTime = 0f;
        startColor = fader.color;
        endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fader.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            yield return null;
        }
    }
}
