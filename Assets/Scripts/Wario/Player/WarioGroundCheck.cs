using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WarioGroundCheck : MonoBehaviour
{
    [Header("Tiles & Tilemaps")]
    public Tilemap ForegroundTilemap;
    public Tilemap BackgroundTilemap;
    public Tilemap ActualTilemap;
    public AnimatedTile powerJumpTile;

    private WarioStateManager stateManager;

    private void OnEnable()
    {
        PlaneSwitcher.OnPlaneSwitched += UpdatePlane;
    }

    private void OnDisable()
    {
        PlaneSwitcher.OnPlaneSwitched -= UpdatePlane;
    }
    private void Start()
    {
        ActualTilemap = ForegroundTilemap;
        stateManager = WarioStateManager.Instance;
    }
    void FixedUpdate()
    {
        Vector3Int cellPosition = ActualTilemap.WorldToCell(transform.position);
        TileBase groundTile = ActualTilemap.GetTile(new Vector3Int(cellPosition.x, cellPosition.y - 1, cellPosition.z));
        Vector3Int roofPosition = new Vector3Int(cellPosition.x, cellPosition.y + 1, cellPosition.z);
        TileBase roofTile = ActualTilemap.GetTile(roofPosition);
        stateManager.IsGrounded = false;
        stateManager.IsOnPowerJump = (groundTile == powerJumpTile);
        stateManager.IsJumping = (!stateManager.IsGrounded && !stateManager.IsOnPowerJump);
        if (groundTile is RockTile groundRock)
        {
            if (groundRock.rockType == RockType.Spike)
            {
                WarioStats.OnUpdateHearts.Invoke(-1);
            }
            else
            {
                stateManager.IsGrounded = true;
            }
        }

        if (roofTile is RockTile rockTile)
        {
            if (rockTile.rockType == RockType.Spike)
            {
                WarioStats.OnHeartsChanged?.Invoke(-1);
            }
            if (stateManager.IsJumping)
            {
                rockTile.Break(roofPosition, ActualTilemap);
                StartCoroutine(BreakCooldown());
            }
        }
    }
        
    private IEnumerator BreakCooldown()
    {
        yield return new WaitForSeconds(0.5f); 
    }
    private void UpdatePlane(bool IsForegroundActive)
    {
        if (IsForegroundActive) ActualTilemap = ForegroundTilemap;
        else ActualTilemap = BackgroundTilemap;
    }
}
