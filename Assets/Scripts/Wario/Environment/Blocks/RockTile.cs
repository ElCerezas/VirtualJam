using UnityEngine;
using UnityEngine.Tilemaps;
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
        if (rockType == RockType.Broken || rockType == RockType.Lucky)
        {
            AudioManager.Instance.PlaySFX("BreakBlock");
            tilemap.SetTile(position, null);
            if (itemsDropped.Length > 0)
            {
                Vector3 worldPosition = tilemap.GetCellCenterWorld(position);
                worldPosition.y++;
                var result = Instantiate(itemsDropped[Random.Range(0, itemsDropped.Length)], worldPosition, Quaternion.identity);
                
                if (tilemap.gameObject.layer == LayerMask.NameToLayer("Background"))
                {
                    result.transform.localScale *= 0.5f;
                }
                result.transform.SetParent(tilemap.transform);
            }
            if (rockType == RockType.Lucky)
            {
                tilemap.SetTile(position, SecondTile);
            }
        }
    }
}
