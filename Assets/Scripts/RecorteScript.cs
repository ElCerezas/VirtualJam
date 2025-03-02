using UnityEngine;

public class RecorteScript : MonoBehaviour
{
    private Vector3 originalScale;
    [SerializeField] private GameObject bigRecorte;
    [SerializeField] private float maxScaleMultiplier = 1.2f; // Tamaño máximo oscilante
    [SerializeField] private float zoomSpeed = 1.5f; // Velocidad de oscilación
    [SerializeField] private float zoomDuration = 0.5f; // Tiempo en segundos para expandir bigRecorte

    private void Start()
    {
        originalScale = transform.localScale;
        StartCoroutine(AnimateScale());
    }

    public void RecorteZoom()
    {
        bigRecorte.SetActive(true);
        bigRecorte.transform.localScale = Vector3.one * 0.1f; // Inicia en escala 0.1
        StartCoroutine(ExpandBigRecorte(() => gameObject.SetActive(false)));
        
    }

    private System.Collections.IEnumerator AnimateScale()
    {
        float t = 0;
        bool growing = true;

        while (true)
        {
            t += Time.deltaTime * zoomSpeed * (growing ? 1 : -1);
            float scaleMultiplier = Mathf.Lerp(1, maxScaleMultiplier, t);
            transform.localScale = originalScale * scaleMultiplier;

            if (t >= 1) growing = false;
            if (t <= 0) growing = true;

            yield return null;
        }
    }

    private System.Collections.IEnumerator ExpandBigRecorte(System.Action onComplete)
    {
        float t = 0;
        Vector3 startScale = Vector3.one * 0.1f;
        Vector3 targetScale = Vector3.one * 1.7f;

        while (t < 1)
        {
            t += Time.deltaTime / zoomDuration;
            bigRecorte.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }
        onComplete.Invoke();
    }
}
