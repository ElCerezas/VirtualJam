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

    public static Action OnSwitchPlane;
    public static Action<bool> OnPlaneSwitched;

    private bool isForegroundActive = true;

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
        SetPlaneCollisions(isForegroundActive);
    }

    private void SwitchPlane()
    {
        isForegroundActive = !isForegroundActive;

        // Cambiar colisiones del jugador
        SetPlaneCollisions(isForegroundActive);
        OnPlaneSwitched?.Invoke(isForegroundActive);
    }

    private void SetPlaneCollisions(bool isForeground)
    {
        if (isForeground) playerCollider.excludeLayers = BackgroundLayer;
        else playerCollider.excludeLayers = ForegroundLayer;
    }
}
