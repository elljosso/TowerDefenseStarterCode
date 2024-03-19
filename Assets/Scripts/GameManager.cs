using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private List<GameObject> archerPrefabs;
    [SerializeField]
    private List<GameObject> swordPrefabs;
    [SerializeField]
    private List<GameObject> wizardPrefabs;
    public GameObject TowerMenu; // GameObject referentie naar het TowerMenu
    private TowerMenu towerMenu; // Script referentie naar het TowerMenu

    public GameObject TopMenu; // GameObject referentie naar het TopMenu
    private TopMenu topMenu; // Script referentie naar het TopMenu

    private ConstructionSite selectedSite; // Variabele om de geselecteerde bouwplaats te onthouden

    private int credits = 0; // Aantal credits van de speler
    private int health = 100; // Gezondheid van de Gate
    private int currentWave = 0; // Huidige wave

    private void Awake()
    {
        // Singleton-patroon: zorg ervoor dat er slechts één instantie van GameManager is
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Haal de TowerMenu-component op van het TowerMenu GameObject
        towerMenu = TowerMenu.GetComponent<TowerMenu>();

        if (towerMenu == null)
        {
            Debug.LogError("TowerMenu script not found on the TowerMenu GameObject!");
        }

        // Haal de TopMenu-component op van het TopMenu GameObject
        topMenu = TopMenu.GetComponent<TopMenu>();

        if (topMenu == null)
        {
            Debug.LogError("TopMenu script not found on the TopMenu GameObject!");
        }

        // Start het spel
        //StartGame();
    }

    public void StartGame()
    {
        // Stel de waarden in voor credits, health en currentWave
        credits = 200;
        health = 10;
        currentWave = 0;

        // Update de labels in het TopMenu met de nieuwe waarden
        topMenu.SetCreditsLabel("Credits: " + credits);
        topMenu.SetHealthLabel("Health: " + health);
        topMenu.SetWaveLabel("Wave: " + currentWave);
    }

    public void SelectSite(ConstructionSite site)
    {
        // Onthoud de geselecteerde bouwplaats
        selectedSite = site;

        // Roep de SetSite-methode aan in het TowerMenu-script en geef de geselecteerde bouwplaats door
        towerMenu.SetSite(selectedSite);
    }

    public void Build(TowerType type, ConstructionSite.SiteLevel level)
    {
        // Je kunt niets bouwen als er geen site is geselecteerd
        if (selectedSite == null)
        {
            Debug.LogWarning("No construction site selected!");
            return;
        }

        List<GameObject> prefabList = null;
        switch (type)
        {
            case TowerType.Archer:
                prefabList = archerPrefabs;
                break;
            case TowerType.Sword:
                prefabList = swordPrefabs;
                break;
            case TowerType.Wizard:
                prefabList = wizardPrefabs;
                break;
        }

        if (prefabList == null)
        {
            Debug.LogWarning("No prefab list found for the selected tower type!");
            return;
        }

        if (level < 0 || (int)level >= prefabList.Count)
        {
            Debug.LogWarning("Invalid level for the selected tower type!");
            return;
        }

        GameObject prefab = prefabList[(int)level-1];
        GameObject tower = Instantiate(prefab, selectedSite.WorldPosition, Quaternion.identity);
        selectedSite.SetTower(tower, level, type);
        towerMenu.SetSite(null); // Verberg het TowerMenu
    }
}