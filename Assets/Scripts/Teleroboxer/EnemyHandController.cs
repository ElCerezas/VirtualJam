using System.Collections;
using UnityEngine;

public class EnemyHandController : MonoBehaviour
{
    public HandStateEnum handState = HandStateEnum.Default;
    private Vector3 originalScale;
    private EnemyHandsSpriteChanger spriteChanger; // Nuevo componente para manejar los sprites

    [SerializeField] private float chargeTime = 1.5f;  // Tiempo de carga antes del golpe
    [SerializeField] private float hitScaleIncrease = 0.2f; // Aumento de tamaño al golpear
    [SerializeField] private float hitDuration = 0.5f;  // Tiempo que la mano permanece agrandada

    private void Start()
    {
        originalScale = transform.localScale;
        spriteChanger = GetComponent<EnemyHandsSpriteChanger>(); // Obtener el manejador de sprites
    }

    /// Inicia la secuencia de carga y golpe.
    public void StartChargeAndHit()
    {
        if (handState == HandStateEnum.Default)
        {
            StartCoroutine(ChargeAndHit());
        }
    }

    /// Maneja la secuencia de carga y golpe.
    private IEnumerator ChargeAndHit()
    {
        // Fase de carga
        handState = HandStateEnum.Charging;
        yield return new WaitForSeconds(chargeTime);

        // Fase de golpe
        handState = HandStateEnum.Hitting;
        transform.localScale = originalScale + Vector3.one * hitScaleIncrease; // Aumenta tamaño

        yield return new WaitForSeconds(hitDuration); // Mantiene el golpe

        // Regresa al estado inicial
        transform.localScale = originalScale;
        handState = HandStateEnum.Default;
    }

    /// Inicia la acción de bloqueo.
    public void StartBlock()
    {
        if (handState == HandStateEnum.Default)
        {
            StartCoroutine(BlockRoutine());
        }
    }

    /// Maneja la acción de bloqueo.
    private IEnumerator BlockRoutine()
    {
        handState = HandStateEnum.Blocking;
        yield return new WaitForSeconds(1.5f); // Duración del bloqueo

        // Vuelve al estado normal
        handState = HandStateEnum.Default;
    }
}