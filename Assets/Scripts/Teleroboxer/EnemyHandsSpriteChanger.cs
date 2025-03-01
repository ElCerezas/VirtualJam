using UnityEngine;

public class EnemyHandsSpriteChanger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private HandState handSprites; // Componente que tiene la lista de sprites
    EnemyHandController ehc;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        handSprites = GetComponent<HandState>();
        ehc = GetComponent<EnemyHandController>();
    }

    private void Update()
    {
        switch (handSprites.handState)
        {
            case HandStateEnum.Default:
                spriteRenderer.sprite = handSprites.hands[0]; // Sprite normal
                break;
            case HandStateEnum.Charging:
                spriteRenderer.sprite = handSprites.hands[2]; // Sprite de carga
                break;
            case HandStateEnum.Hitting:
                spriteRenderer.sprite = handSprites.hands[0]; // Sprite normal (o cambiar si hay uno especial para golpear)
                break;
            case HandStateEnum.Blocking:
                spriteRenderer.sprite = handSprites.hands[1]; // Sprite de bloqueo
                break;
        }
    }
}

