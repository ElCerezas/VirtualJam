using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BoxingDamageManager : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] GameObject camara;
    [SerializeField] float fadeDuration = 0.5f;

    public void OnDamageRecived()
    {
        StartCoroutine(ShakeCamera(0.3f, 0.5f));
        StartCoroutine(FadeInOut());
    }
    public void OnJustShake(float time, float force)
    {
        StartCoroutine(ShakeCamera(time, force));
    }

    private IEnumerator ShakeCamera(float shaky, float shakyStrengh)
    {
        Vector3 originalPosition = camara.transform.localPosition;
        float elapsed = 0f;

        while (elapsed < shaky)
        {
            float x = Random.Range(-shakyStrengh, shakyStrengh);
            float y = Random.Range(-shakyStrengh, shakyStrengh);

            camara.transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        camara.transform.localPosition = originalPosition;
    }
    private IEnumerator FadeInOut()
    {
        Color color = image.color;

        // Fade in
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            color.a = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            image.color = color;
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Fade out
        elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            color.a = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            image.color = color;
            elapsed += Time.deltaTime;
            yield return null;
        }

        color.a = 0f;
        image.color = color;
    }
}
