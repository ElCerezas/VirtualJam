using UnityEngine;
using UnityEngine.EventSystems;

public class DialogSelectionHandler : MonoBehaviour
{
    [SerializeField] private GameObject buttonToSelect;
    private EventSystem eventSystem;
    private GameManager gameManager;
    PauseManager pauseManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        pauseManager = gameManager.gameObject.GetComponent<PauseManager>();
        if (gameManager != null)
        {
            eventSystem = gameManager.GetComponent<EventSystem>(); // Obtiene el EventSystem desde GameManager
        }

        if (eventSystem == null)
        {
            Debug.LogError("No se encontró un EventSystem en el GameManager.");
        }
    }
    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if ((Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f) && !pauseManager.isPaused)
        {
            Debug.Log("MoveReselect");
            SelectNewButton();
        }

    }
    public void SelectNewButton()
    {
        if (eventSystem != null)
        {
            // Si no hay un GameObject seleccionado o el actual está desactivado
            if (eventSystem.currentSelectedGameObject == null || !eventSystem.currentSelectedGameObject.activeInHierarchy)
            {
                eventSystem.SetSelectedGameObject(buttonToSelect);
            }
        }
    }
}
