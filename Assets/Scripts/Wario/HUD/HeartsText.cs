using UnityEngine;
using UnityEngine.UI;

public class HeartsText : MonoBehaviour
{
    private Text text;

    private void OnEnable()
    {
        WarioStats.OnHeartsChanged += UpdateText;
    }

    private void OnDisable()
    {
        WarioStats.OnHeartsChanged -= UpdateText;
    }

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void UpdateText(int Hearts)
    {
        text.text = "X " + Hearts;
    }
}
