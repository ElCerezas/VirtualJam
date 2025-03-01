using UnityEngine;

public class HeartScript : MonoBehaviour
{
    public int HeartsModifier;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            WarioStats.OnUpdateHearts(HeartsModifier);
            Destroy(gameObject);
        }
    }
}
