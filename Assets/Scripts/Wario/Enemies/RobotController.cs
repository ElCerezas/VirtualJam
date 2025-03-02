using UnityEngine;

public class RobotController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] public Transform startPoint;
    [SerializeField] public Transform endPoint;
    [SerializeField] private float speed = 1.0f;

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

        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, tMovement);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            WarioStats.OnUpdateHearts?.Invoke(-1);
        }
    }
}