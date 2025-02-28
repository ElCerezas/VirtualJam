using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
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
    public void NextScene()
    {
        PlayerPrefs.SetInt("chapterIndex", 0);
        PlayerPrefs.SetInt("dialogIndex", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void NarrativeGoToGame(int scene)
    {
        PlayerPrefs.SetInt("dialogIndex", NarrativeManager.dialogIndex);
        PlayerPrefs.SetInt("chapterIndex", NarrativeManager.chapterIndex);
        SceneManager.LoadScene(scene);
    }
    public static void GameWin()
    {
        PlayerPrefs.SetInt("chapterIndex", PlayerPrefs.GetInt("chapterIndex")+1);
        PlayerPrefs.SetInt("dialogIndex", 0);
        SceneManager.LoadScene("Narrative");
    }
    public static void GameLose()
    {
        PlayerPrefs.SetInt("dialogIndex", PlayerPrefs.GetInt("dialogIndex"));
        SceneManager.LoadScene("Narrative");
    }
    public int GetSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    private void OnDrawGizmos()
    {
        string value = PlayerPrefs.GetInt("chapterIndex", 0).ToString(); // Cambia a GetFloat o GetString si es necesario
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.blue;
        style.fontSize = (int)(1 * 10);

        Vector3 position = transform.position + Vector3.up * 1.5f; // Ajusta la posición del texto
        UnityEditor.Handles.Label(position, $"v: {value}", style);
    }
}
