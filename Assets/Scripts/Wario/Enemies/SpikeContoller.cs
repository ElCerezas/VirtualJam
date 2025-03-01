using UnityEngine;

public class SpikeController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] public Transform startPoint;
    [SerializeField] public Transform endPoint;
    [SerializeField] private float speed = 1.0f;

    [Header("Scale Settings")]
    [SerializeField] private Vector3 startScale = Vector3.one;
    [SerializeField] private Vector3 endScale = Vector3.one * 0.5f;
    [SerializeField] private float scaleSpeed = 1.0f;

    private void Start()
    {
        // Initialize the start point and scale if not set
        if (startPoint.position == Vector3.zero)
        {
            startPoint.position = transform.position;
        }
    }

    private void Update()
    {
        // Calculate the time-based factor
        float tMovement = Mathf.PingPong(Time.time * speed, 1.0f);

        // Calculate the time-based factor for scaling
        float tScale = Mathf.PingPong(Time.time * scaleSpeed, 1.0f);

        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, tMovement);
        transform.localScale = Vector3.Lerp(startScale, endScale, tScale);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && transform.localScale.magnitude > (endScale.magnitude / 2))
        {
            WarioStats.OnUpdateHearts?.Invoke(-1);
        }
    }
}
