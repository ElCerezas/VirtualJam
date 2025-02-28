using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class GifManager : MonoBehaviour
{
    [SerializeField] int maxDialog, maxScene;
    [SerializeField] float speed = 0.2f;
    [SerializeField] Sprite[] frames;
    int i;
    Image img;
    float elapsedTime = 0f;
    private void Start()
    {
        img = GetComponent<Image>();
    }
    private void Update()
    {
        if (elapsedTime < speed)
        {
            elapsedTime += Time.deltaTime;
            Debug.Log(elapsedTime);
        }
        else
        {
            elapsedTime = 0f;
            i++;
            i = i > frames.Length ? 0 : i;
            img.sprite = frames[i];
        }
        if (NarrativeManager.chapterIndex > maxScene || NarrativeManager.dialogIndex > maxDialog)
        {
            gameObject.SetActive(false);
        }

    }
}
