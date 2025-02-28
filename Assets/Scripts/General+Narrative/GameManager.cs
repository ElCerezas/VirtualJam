using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int savedChapter = 0;
    public int savedDialog = 0;
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
    }
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void NarrativeGoToGame(int scene)
    {
        savedDialog = NarrativeManager.dialogIndex;
        savedChapter = NarrativeManager.chapterIndex;
        SceneManager.LoadScene(scene);
    }
    public void GameWin()
    {
        savedChapter++;
        savedDialog = 0;
    }
    public int GetSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
