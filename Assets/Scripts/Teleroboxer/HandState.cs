using System.Collections;
using UnityEngine;

public enum HandStateEnum
{
    Default, Charging, Hitting, Blocking
}

public class HandState : MonoBehaviour
{
    public HandStateEnum handState = HandStateEnum.Default;

    [SerializeField] public Sprite[] hands; // 0->Default, 1->Blocking
    private SpriteRenderer spriteRenderer;

    [SerializeField] private bool playerHand, isLeftHand;

    // Hit logic
    private Vector3 startPos;
    private bool isMoving = false;

    [SerializeField] private float hitDuration = 0.5f;
    [SerializeField] private float hitHeight = 1.0f;
    [SerializeField] private float hitAmplitude = 2.0f;

    private Coroutine hitCoroutine;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPos = transform.position;
    }

    private void Update()
    {
        spriteRenderer.sprite = handState == HandStateEnum.Blocking ? hands[1] : hands[0];
    }

    public void OnBlock(bool blocking)
    {
        handState = blocking ? HandStateEnum.Blocking : HandStateEnum.Default;
    }

    public void OnHit()
    {
        if (playerHand && handState == HandStateEnum.Default && !isMoving)
        {
            handState = HandStateEnum.Hitting;
            hitCoroutine = StartCoroutine(ParabolicMovement());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (handState != HandStateEnum.Hitting) return; // Solo procesar colisión si está golpeando

        if (collision.CompareTag("EnemyHand"))
        {
            HandState otherHand = collision.GetComponent<HandState>();
            if (otherHand != null && otherHand.handState == HandStateEnum.Blocking)
            {
                CancelHit();
            }
        }
        else if (collision.CompareTag("Enemy"))
        {
            CancelHit();
        }
    }

    private IEnumerator ParabolicMovement()
    {
        isMoving = true;
        Vector3 endPos = startPos + (isLeftHand ? Vector3.right : Vector3.left) * hitAmplitude;
        float elapsedTime = 0f;

        while (elapsedTime < hitDuration && handState == HandStateEnum.Hitting)
        {
            float t = elapsedTime / hitDuration;
            float height = Mathf.Sin(t * Mathf.PI) * hitHeight;
            transform.position = Vector3.Lerp(startPos, endPos, t) + Vector3.up * height;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        CancelHit();
    }

    private void CancelHit()
    {
        if (hitCoroutine != null) StopCoroutine(hitCoroutine);
        transform.position = startPos;
        handState = HandStateEnum.Default;
        isMoving = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 endPos = transform.position + (isLeftHand ? Vector3.right : Vector3.left) * hitAmplitude;
        Gizmos.DrawLine(transform.position, endPos);

        for (float t = 0; t <= 1; t += 0.1f)
        {
            float height = Mathf.Sin(t * Mathf.PI) * hitHeight;
            Vector3 point = Vector3.Lerp(transform.position, endPos, t) + Vector3.up * height;
            Gizmos.DrawSphere(point, 0.1f);
        }
    }
}
