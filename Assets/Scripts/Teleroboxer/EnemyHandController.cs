using System.Collections;
using UnityEngine;

public class EnemyHandController : MonoBehaviour
{
    HandState handState;
    private Vector3 originalPosition; // Almacena la posición original de la mano
    private Vector3 originalScale;
    private EnemyHandsSpriteChanger spriteChanger; // Maneja el cambio de sprites

    [SerializeField] private float chargeTime = 1.5f;  // Tiempo de carga antes del golpe
    [SerializeField] private float hitScaleIncrease = 0.2f; // Aumento de tamaño al golpear
    [SerializeField] private float hitDuration = 0.5f;  // Tiempo que la mano permanece agrandada
    [SerializeField] private float blockYOffset = 0.5f; // Distancia que sube la mano al bloquear
    [SerializeField] HandState handToCheck;

    private void Start()
    {
        originalScale = transform.localScale;
        originalPosition = transform.position; // Guarda la posición original
        spriteChanger = GetComponent<EnemyHandsSpriteChanger>(); // Obtener el manejador de sprites
        handState = GetComponent<HandState>();
    }

    /// Inicia la secuencia de carga y golpe.
    public void StartChargeAndHit()
    {
        if (handState.handState == HandStateEnum.Default)
        {
            StartCoroutine(ChargeAndHit());
        }
    }

    /// Maneja la secuencia de carga y golpe.
    private IEnumerator ChargeAndHit()
    {
        // Fase de carga
        handState.handState = HandStateEnum.Charging;
        yield return new WaitForSeconds(chargeTime);

        if (handToCheck.handState == HandStateEnum.Blocking)
        {
            Debug.Log("¡El jugador ha bloqueado el golpe!");
        }
        else
        {
            Debug.Log("¡Golpe exitoso al jugador!");
        }

        // Fase de golpe
        handState.handState = HandStateEnum.Hitting;
        transform.localScale = originalScale + Vector3.one * hitScaleIncrease; // Aumenta tamaño

        yield return new WaitForSeconds(hitDuration); // Mantiene el golpe

        // Regresa al estado inicial
        transform.localScale = originalScale;
        handState.handState = HandStateEnum.Default;
    }


    /// Inicia la acción de bloqueo.
    public void StartBlock()
    {
        if (handState.handState == HandStateEnum.Default)
        {
            StartCoroutine(BlockRoutine());
        }
    }

    /// Maneja la acción de bloqueo.
    private IEnumerator BlockRoutine()
    {
        handState.handState = HandStateEnum.Blocking;
        transform.position = originalPosition + new Vector3(0, blockYOffset, 0); // Sube la mano en Y

        yield return new WaitForSeconds(1.5f); // Duración del bloqueo

        // Vuelve al estado normal
        handState.handState = HandStateEnum.Default;
        transform.position = originalPosition; // Devuelve la mano a su posición original
    }
}
