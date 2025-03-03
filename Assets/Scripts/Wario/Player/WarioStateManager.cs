using UnityEngine;

public class WarioStateManager : MonoBehaviour
{
    public static WarioStateManager Instance { get; private set; } // Singleton

    [Header("Player estates")]
    public bool IsGrounded = false;
    public bool IsOnPowerJump = false;
    public bool IsJumping = false;
    public bool IsDashing = false;

    public bool IsInForeground = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } 
    }
}
