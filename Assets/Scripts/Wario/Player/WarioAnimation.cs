using System;
using UnityEngine;

public class WarioAnimation : MonoBehaviour
{
    private Animator animator;
    private WarioStateManager stateManager;
    private WarioMovement movement;

    public static Action OnJumpStarted;

    private void OnEnable()
    {
        OnJumpStarted += StartJump;
    }

    private void OnDisable()
    {
        OnJumpStarted -= StartJump;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        stateManager = WarioStateManager.Instance;
        movement = GetComponent<WarioMovement>();
    }

    void Update()
    {
        animator.SetFloat("xVelocity", Mathf.Abs(movement.xVelocity));
        animator.SetBool("IsJumping", stateManager.IsJumping);
        animator.SetFloat("yVelocity", 1);
    }

    private void StartJump()
    {
        //animator.SetFloat()
    }
}
