using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaneSwitcher : MonoBehaviour
{
    [Header("Planes")]
    public GameObject ForegroundPlane;
    public GameObject BackgroundPlane;

    [Header("Player Settings")]
    public LayerMask ForegroundLayer;
    public LayerMask BackgroundLayer;
    private Collider2D playerCollider;

    private WarioStateManager stateManager;

    public static Action OnSwitchPlane;
    public static Action<bool> OnPlaneSwitched;

    private void OnEnable()
    {
        OnSwitchPlane += SwitchPlane;
    }

    private void OnDisable()
    {
        OnSwitchPlane -= SwitchPlane;
    }

    private void Awake()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        SetPlaneCollisions(true);
    }

    private void Start()
    {
        stateManager = WarioStateManager.Instance;
    }

    private void SwitchPlane()
    {
        stateManager.IsInForeground = !stateManager.IsInForeground;

        // Cambiar colisiones del jugador
        SetPlaneCollisions(stateManager.IsInForeground);
        OnPlaneSwitched?.Invoke(stateManager.IsInForeground);
    }

    private void SetPlaneCollisions(bool isForeground)
    {
        if (isForeground) playerCollider.excludeLayers = BackgroundLayer;
        else playerCollider.excludeLayers = ForegroundLayer;
    }
}
