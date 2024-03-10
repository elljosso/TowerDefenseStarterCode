using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSite : MonoBehaviour
{
    public Vector3Int TilePosition { get; private set; }
    public Vector3 WorldPosition { get; private set; }
    public SiteLevel Level { get; private set; }
    public TowerType TowerType { get; private set; }

    private GameObject tower;

    public ConstructionSite(Vector3Int tilePosition, Vector3 worldPosition, SiteLevel level, TowerType towerType)
    {
        TilePosition = tilePosition;
        WorldPosition = worldPosition;
        Level = level;
        TowerType = towerType;
    }
    public ConstructionSite(Vector3Int tilePosition, Vector3 worldPosition)
    {
        // Wijs de tilePosition en worldPosition toe
        TilePosition = tilePosition;
        WorldPosition = worldPosition + Vector3.up * 0.5f; // Pas de y-waarde aan

        // Stel tower gelijk aan null
        tower = null;
    }
    public void SetTower(GameObject newTower, SiteLevel newLevel, TowerType newType)
    {
        // Controleer of de huidige tower verschillend is van null
        if (tower != null)
        {
            // Als dat zo is, verwijder het bestaande gameobject
            GameObject.Destroy(tower);
        }

        // Wijs de nieuwe tower, level en type toe
        tower = newTower;
        Level = newLevel;
        TowerType = newType;
    }
}
