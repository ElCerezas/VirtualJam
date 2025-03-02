using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public int CoinsModifier;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX("Coin");
            WarioStats.OnUpdateCoins(CoinsModifier);
            Destroy(gameObject);
        }
    }
}