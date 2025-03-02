using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WarioMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 30f;
    [SerializeField] private float powerJumpForce = 30f;

    private Rigidbody2D rb;
    private Transform player;
    private WarioStateManager stateManager;

    public float xVelocity;
    public float yVelocity;
    
    void Awake()
    {
        player = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        stateManager = WarioStateManager.Instance;
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = xVelocity * moveSpeed;
        //yVelocity = rb.linearVelocityY;
    }

    public void OnJump()
    {
        if (stateManager != null)
        {
            if (stateManager.IsGrounded)
            {
                StartJump();
            }
            else if (stateManager.IsOnPowerJump)
            {
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
        AudioManager.Instance.PlaySFX("Jump");
    }

    private IEnumerator SwitchPlane()
    {
        yield return new WaitForSeconds(0.5f);
        player.localScale = player.localScale / 2;
        PlaneSwitcher.OnSwitchPlane.Invoke();
    }

    private void OnMove(InputValue value)
    {
        var inputVal = value.Get<Vector2>();
        xVelocity = inputVal.x;
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