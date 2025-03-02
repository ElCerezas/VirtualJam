using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.RuleTile.TilingRuleOutput;

public enum RockType
{
    Ground,
    Normal,
    Broken,
    Lucky,
    Fire,
    Solid,
    Giant,
    Spike
}

[CreateAssetMenu(fileName = "RocaTile", menuName = "Tiles/Rock")]
public class RockTile : TileBase
{
    public Sprite tileSprite;
    public RockType rockType;
    public GameObject[] itemsDropped;
    public RockTile SecondTile;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = tileSprite;
        tileData.colliderType = Tile.ColliderType.Sprite;
    }

    public void Break(Vector3Int position, Tilemap tilemap)
    {
        if (rockType == RockType.Broken)
        {
            AudioManager.Instance.PlaySFX("BreakBlock");
            tilemap.SetTile(position, null);
            if (itemsDropped.Length > 0)
            {
                Vector3 worldPosition = tilemap.GetCellCenterWorld(position);
                var result = Instantiate(itemsDropped[Random.Range(0, itemsDropped.Length)], worldPosition, Quaternion.identity);
            }
        }
        else if (rockType == RockType.Lucky)
        {
            AudioManager.Instance.PlaySFX("BreakBlock");
            tilemap.SetTile(position, null);
            if (itemsDropped.Length > 0)
            {
                Vector3 worldPosition = tilemap.GetCellCenterWorld(position);
                var result = Instantiate(itemsDropped[Random.Range(0, itemsDropped.Length)], worldPosition, Quaternion.identity);
            }
            tilemap.SetTile(position, SecondTile);
        }
    }
}
