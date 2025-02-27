using System.Collections;
using UnityEngine;

public class PlayerHandController : MonoBehaviour
{
    public HandState handState;

    private SpriteRenderer spriteRenderer;
    private Vector3 originalPosition;
    private Vector3 originalScale;

    [SerializeField] private Sprite[] hands; // 0->Default, 1->Blocking
    [SerializeField] private bool isLeftHand;
    [SerializeField] private float hitDuration = 0.5f;
    [SerializeField] private float hitHeight = 1.0f;
    [SerializeField] private float hitAmplitude = 2.0f;

    [Header("Blocking Settings")]
    [SerializeField] private float blockYOffset = 0.5f; // Cuánto sube la mano al bloquear
    [SerializeField] private float blockXOffset = 0.2f; // Desplazamiento en X al bloquear
    [SerializeField] private float blockScaleIncrease = 0.3f; // Cuánto se agranda al bloquear

    BoxerUIController boxerUIController;
    private Coroutine hitCoroutine;
    private bool isMoving = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalPosition = transform.position;
        originalScale = transform.localScale;
        handState = GetComponent<HandState>();
        boxerUIController = GameObject.FindGameObjectWithTag("Respawn").GetComponent<BoxerUIController>();
    }

    private void Update()
    {
        spriteRenderer.sprite = handState.handState == HandStateEnum.Blocking ? hands[1] : hands[0];
    }

    public void OnBlock(bool blocking)
    {
        if (handState.handState == HandStateEnum.Default || handState.handState == HandStateEnum.Blocking)
        {
            handState.handState = blocking ? HandStateEnum.Blocking : HandStateEnum.Default;

            if (blocking)
            {
                // Cambia la posición y tamaño al bloquear
                float xOffset = isLeftHand ? -blockXOffset : blockXOffset;
                transform.position = originalPosition + new Vector3(xOffset, blockYOffset, 0);
                transform.localScale = originalScale * (1 + blockScaleIncrease);
            }
            else
            {
                // Vuelve al tamaño y posición originales
                transform.position = originalPosition;
                transform.localScale = originalScale;
            }
        }
    }

    public void OnHit()
    {
        if (handState.handState == HandStateEnum.Default && !isMoving)
        {
            handState.handState = HandStateEnum.Hitting;
            hitCoroutine = StartCoroutine(ParabolicMovement());
        }
    }

    private IEnumerator ParabolicMovement()
    {
        isMoving = true;
        Vector3 endPos = originalPosition + (isLeftHand ? Vector3.right : Vector3.left) * hitAmplitude;
        float elapsedTime = 0f;

        while (elapsedTime < hitDuration && handState.handState == HandStateEnum.Hitting)
        {
            float t = elapsedTime / hitDuration;
            float height = Mathf.Sin(t * Mathf.PI) * hitHeight;
            transform.position = Vector3.Lerp(originalPosition, endPos, t) + Vector3.up * height;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        CancelHit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (handState.handState != HandStateEnum.Hitting) return;

        if (collision.CompareTag("EnemyHand"))
        {
            HandState enemyHand = collision.GetComponent<HandState>();
            if (enemyHand != null && enemyHand.handState == HandStateEnum.Blocking)
            {
                CancelHit();
                Debug.Log("Golpe bloqueado por el enemigo.");
                boxerUIController.OnHit(true, false);
            }
        }
        else if (collision.CompareTag("Enemy"))
        {
            CancelHit();
            Debug.Log("Golpe impactado en el enemigo.");
            boxerUIController.OnHit(false, false);
        }
    }

    private void CancelHit()
    {
        if (hitCoroutine != null) StopCoroutine(hitCoroutine);
        transform.position = originalPosition;
        handState.handState = HandStateEnum.Default;
        isMoving = false;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.red;
        Vector3 endPos = originalPosition + (isLeftHand ? Vector3.right : Vector3.left) * hitAmplitude;

        Gizmos.DrawLine(originalPosition, endPos);

        for (float t = 0; t <= 1; t += 0.1f)
        {
            float height = Mathf.Sin(t * Mathf.PI) * hitHeight;
            Vector3 point = Vector3.Lerp(originalPosition, endPos, t) + Vector3.up * height;
            Gizmos.DrawSphere(point, 0.1f);
        }
    }
}
