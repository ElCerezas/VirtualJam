using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WarioMovement : MonoBehaviour
{
    public static WarioMovement Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private float foreMoveSpeed = 5f;
    [SerializeField] private float foreJumpForce = 20f;

    [SerializeField] private float backMoveSpeed = 3f;
    [SerializeField] private float backJumpForce = 15f;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private Rigidbody2D rb;
    private Transform player;
    private WarioStateManager stateManager;

    public float xVelocity;
    public float yVelocity;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            player = GetComponent<Transform>();
            rb = GetComponent<Rigidbody2D>();
        }
    }
    private void Start()
    {
        stateManager = WarioStateManager.Instance;
        moveSpeed = foreMoveSpeed;
        jumpForce = foreJumpForce;
        transform.localScale = new Vector3(1f, 1f, 0.16f);
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = xVelocity * moveSpeed;
    }

    public void OnJump()
    {
        if (stateManager != null)
        {
            if (stateManager.IsGrounded)
            {
                AudioManager.Instance.PlaySFX("Jump");
                StartJump();
            }
            else if (stateManager.IsOnPowerJump)
            {
                AudioManager.Instance.PlaySFX("PowerUp");
                StartJump();
                StartCoroutine(SwitchPlane());
            }
        }
    }

    private void StartJump()
    {
        rb.linearVelocityY = 0;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        WarioAnimation.OnJumpStarted.Invoke();
    }

    private IEnumerator SwitchPlane()
    {
        yield return new WaitForSeconds(0.5f);
        PlaneSwitcher.OnSwitchPlane.Invoke();
        if (stateManager.IsInForeground)
        {
            moveSpeed = foreMoveSpeed;
            jumpForce = foreJumpForce;
            transform.localScale = new Vector3(1f, 1f, 0.16f);
        }
        else
        {
            moveSpeed = backMoveSpeed;
            jumpForce = backJumpForce;
            transform.localScale = new Vector3(0.5f, 0.5f, 0.16f);
        }
    }

    private void OnMove(InputValue value)
    {
        var inputVal = value.Get<float>();
        xVelocity = inputVal;
        FlipGameObject();
    }

    private void FlipGameObject()
    {
        if (xVelocity < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (xVelocity > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}