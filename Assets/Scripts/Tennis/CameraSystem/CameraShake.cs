using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float duration = 0.2f;
    public float magnitude = 0.1f;

    public IEnumerator Shake()
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }

    public void StartShake()
    {
        StartCoroutine(Shake());
    }
}
