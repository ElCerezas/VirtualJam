using UnityEngine;

public class WarioDash : MonoBehaviour
{
    [Header("Configuraci�n")]
    public float dashSpeed = 20f;
    public float dashDuration = 2f;
    public float dashCooldown = 1f;

    private float dashEndTime = 4f;
    private float nextDashTime = 4f;

    private Rigidbody2D rb;
    private WarioStateManager stateManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
            rb.linearVelocityX *= dashSpeed;
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
    }

    private void StopDash()
    {
        stateManager.IsDashing = false;
        rb.linearVelocity = Vector2.zero;
    }
}
