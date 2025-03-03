using UnityEngine;

public class AfterimageEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float fadeSpeed;

    public void Initialize(float speed)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fadeSpeed = speed;
    }

    void Update()
    {
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a -= fadeSpeed * Time.deltaTime;
            spriteRenderer.color = color;

            if (color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}