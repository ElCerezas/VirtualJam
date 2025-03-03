using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private float maxY;   // Altura máxima
    [SerializeField] private float speed = 1f; // Velocidad de subida
    RectTransform rectTransform;
    float actualSpeed;
    private void Start()
    {

        rectTransform = GetComponent<RectTransform>();
        actualSpeed = speed;
    }

    private void Update()
    {
        if (rectTransform.position.y < maxY)
        {
            transform.position += Vector3.up * actualSpeed * Time.deltaTime;
        }
    }
}
