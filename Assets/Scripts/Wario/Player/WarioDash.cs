using System.Collections;
using UnityEngine;

public class WarioDash : MonoBehaviour
{
    [Header("Configuración")]
    public float dashSpeed = 20f;
    public float dashDuration = 2f;
    public float dashCooldown = 1f;
    public float fadeSpeed = 0.1f;
    float trailInterval = 0.1f;

    private float dashEndTime;
    private float nextDashTime;
    private float nextTrailTime;

    private Rigidbody2D rb;
    private WarioStateManager stateManager;
    private WarioMovement movement;
    private SpriteRenderer playerSprite;

    private int updateCounter = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        stateManager = WarioStateManager.Instance;
        movement = WarioMovement.Instance;
        nextDashTime = Time.time;
        playerSprite = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        updateCounter++;
        if (stateManager.IsDashing)
        {
            if (!stateManager.IsJumping && Time.time >= dashEndTime)
            {
                StopDash();
            }
            if (Time.time >= nextTrailTime)
            {
                CreateTrail();
                nextTrailTime = Time.time + trailInterval;
            }
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
        if (!stateManager.IsJumping && Time.time >= nextDashTime)
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
    }

    private void CreateTrail()
    {
        GameObject trail = new GameObject("Trail");
        trail.transform.position = transform.position;
        trail.transform.rotation = transform.rotation;
        trail.transform.localScale = transform.localScale;
        trail.transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
        //trail.transform.SetParent(transform);

        SpriteRenderer trailSprite = trail.AddComponent<SpriteRenderer>();
        trailSprite.sprite = playerSprite.sprite;
        trailSprite.sortingOrder = playerSprite.sortingOrder - 1;

        trail.AddComponent<AfterimageEffect>().Initialize(fadeSpeed);
    }
}
