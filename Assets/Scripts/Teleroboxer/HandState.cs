using System.Collections;
using UnityEngine;

public enum HandStateEnum
{
    Default, Charging, Hitting, Blocking
}

public class HandState : MonoBehaviour
{
    public HandStateEnum handState = HandStateEnum.Default;

    [SerializeField] public Sprite[] hands; // 0->Default, 1->Blocking
    private SpriteRenderer spriteRenderer;

    [SerializeField] private bool playerHand, isLeftHand;
}