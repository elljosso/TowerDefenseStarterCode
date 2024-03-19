using UnityEngine;

public class ConstructionSite
{
    public Vector3Int TilePosition { get; private set; }
    public Vector3 WorldPosition { get; private set; }
    public SiteLevel Level { get; private set; } = SiteLevel.Onbebouwd;
    public TowerType TowerType { get; private set; }

    private GameObject tower;

    public enum SiteLevel
    {
        Onbebouwd,
        Level1,
        Level2,
        Level3
    }

    public ConstructionSite(Vector3Int tilePosition, Vector3 worldPosition)
    {
        // Wijs de tilePosition en worldPosition toe
        TilePosition = tilePosition;
        WorldPosition = new Vector3(worldPosition.x, worldPosition.y + 0.5f, worldPosition.z); // Pas de y-waarde aan
        // Stel tower gelijk aan null
        tower = null;
    }

    public void SetTower(GameObject tower, SiteLevel level, TowerType type)
    {
        // Voordat je de tower toewijst, controleer of de huidige tower verschillend is van null
        if (this.tower != null)
        {
            // Als dat zo is, vernietig het huidige gameobject
            GameObject.Destroy(this.tower);
        }

        // Wijs de nieuwe tower, level en type toe
        this.tower = tower;
        Level = level;
        TowerType = type;
    }
}