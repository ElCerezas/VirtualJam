using UnityEngine;
using UnityEngine.UI;

public class CoinsText : MonoBehaviour
{
    private Text text;

    private void OnEnable()
    {
        WarioStats.OnCoinsChanged += UpdateText;
    }

    private void OnDisable()
    {
        WarioStats.OnCoinsChanged -= UpdateText;
    }

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void UpdateText(int Coins)
    {
        text.text = "X " + Coins;
    }
}
