using UnityEngine;

public class GameOver : MonoBehaviour
{
    GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }
    public void OnReturnTitle()
    {
        PlayerPrefs.SetInt("chapterIndex", 0);
        PlayerPrefs.SetInt("dialogIndex", 0);
        gameManager.GoToScene("Title");
    }
    public void OnEnding()
    {
        PlayerPrefs.SetInt("chapterIndex", 0);
        PlayerPrefs.SetInt("dialogIndex", 0);
        gameManager.GoToScene("Credits");
    }
}
