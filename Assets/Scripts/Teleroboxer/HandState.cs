using UnityEngine;

public enum handState
{
    Default, Charging, Hiting, Blocking
}
public class HandState : MonoBehaviour
{
    public handState handState = handState.Default;
    [SerializeField] Sprite[] hands; // 0->Default//Punching 1-> 1->Blocking
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (handState != handState.Blocking)
        {
            spriteRenderer.sprite = hands[0];
        } 
        else
        {
            spriteRenderer.sprite = hands[1];
        }
    }
    public void OnBlock(bool blocking)
    {
        if (blocking)
        {
            handState = handState.Blocking;
        }
        else
        {
            handState = handState.Default;
        }
    }
}
