using System;
using UnityEngine;

public class WarioAnimation : MonoBehaviour
{
    private Animator animator;
    private WarioStateManager stateManager;
    private WarioMovement movement;

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
}
