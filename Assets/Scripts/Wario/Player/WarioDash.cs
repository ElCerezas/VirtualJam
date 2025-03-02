using UnityEngine;

public class WarioDash : MonoBehaviour
{
    [Header("Configuraci�n")]
    public float dashSpeed = 20f;
    public float dashDuration = 2f;
    public float dashCooldown = 1f;

    private float dashEndTime = 4f;
    private float nextDashTime = 4f;
    private Vector2 dashDirection;

    private Rigidbody2D rb;
    private WarioMovement movement;
    private WarioStateManager stateManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<WarioMovement>();
    }

    private void Start()
    {
        stateManager = WarioStateManager.Instance;
    }

    private void Update()
    {
        if (stateManager.IsDashing && Time.time >= dashEndTime)
        {
            StopDash();
        }
    }

    private void FixedUpdate()
    {
        if (stateManager.IsDashing)
        {
            rb.linearVelocity *= dashSpeed;
        }
    }

    private void OnDash()
    {
        if (Time.time >= nextDashTime)
        {
            StartDash();
        }
    }

    private void StartDash()
    {
        stateManager.IsDashing = true;
        dashEndTime = Time.time + dashDuration;
        nextDashTime = Time.time + dashCooldown;
        //movement.enabled = false;
    }

    private void StopDash()
    {
        stateManager.IsDashing = false;
        rb.linearVelocity = Vector2.zero;
        //movement.enabled = true;
    }

}
