using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Configuration")]
    public Transform Player;
    public float SmoothSpeed = 0.125f;
    public Vector3 Offset;

    [Header("Limits")]
    public float minX;
    public float maxX;

    void LateUpdate()
    {
        if (Player != null)
        {

            // We get desired camera position (Only x axis)
            Vector3 desiredPosition = new Vector3(Player.position.x + Offset.x, transform.position.y, transform.position.z);

            // Limit the position inside the limits
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);

            // Smooth camera movement
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
