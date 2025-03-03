using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
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
        PlayerPrefs.GetInt("chapterIndex", 0);
        PlayerPrefs.GetInt("dialogIndex", 0);
    }
    public void NewGame()
    {
        PlayerPrefs.SetInt("chapterIndex", 0);
        PlayerPrefs.SetInt("dialogIndex", 0);
        SceneManager.LoadScene("Narrative");
    }
    public void NarrativeGoToGame(int scene)
    {
        PlayerPrefs.SetInt("dialogIndex", NarrativeManager.dialogIndex);
        PlayerPrefs.SetInt("chapterIndex", NarrativeManager.chapterIndex);
        SceneManager.LoadScene(scene);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public static void GameWin()
    {
        int prevChapter = PlayerPrefs.GetInt("chapterIndex");
        PlayerPrefs.SetInt("chapterIndex", prevChapter + 1);
        PlayerPrefs.SetInt("dialogIndex", 0);
        SceneManager.LoadScene("Narrative");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public static void GameLose()
    {
        int prevDialog = PlayerPrefs.GetInt("dialogIndex");
        Debug.LogWarning($"Prev dialog: {PlayerPrefs.GetInt("dialogIndex")} PrevChapter: {PlayerPrefs.GetInt("chapterIndex")}");
        PlayerPrefs.SetInt("dialogIndex", prevDialog);
        SceneManager.LoadScene("Narrative");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public int GetSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
    public void GoToScene(string scene)
    {
        Debug.Log("To the end");
        PlayerPrefs.SetInt("dialogIndex", NarrativeManager.dialogIndex);
        PlayerPrefs.SetInt("chapterIndex", NarrativeManager.chapterIndex);
        SceneManager.LoadScene(scene);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
