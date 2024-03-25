using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private bool waveActive = false;
    private int currentWave = 0;
    private int enemyInGameCounter = 0; // Variabele voor het bijhouden van het aantal vijanden in het spel

    [SerializeField]
    private List<GameObject> archerPrefabs;
    [SerializeField]
    private List<GameObject> swordPrefabs;
    [SerializeField]
    private List<GameObject> wizardPrefabs;

    public GameObject TowerMenu;
    private TowerMenu towerMenu;
    public GameObject TopMenu;
    private TopMenu topMenu;
    private ConstructionSite selectedSite;
    private int credits = 100;
    private int health = 10;

    private void Awake()
    {
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
        towerMenu = TowerMenu.GetComponent<TowerMenu>();
        if (towerMenu == null)
        {
            Debug.LogError("TowerMenu script not found on the TowerMenu GameObject!");
        }

        topMenu = TopMenu.GetComponent<TopMenu>();
        if (topMenu == null)
        {
            Debug.LogError("TopMenu script not found on the TopMenu GameObject!");
        }
    }

    public void StartGame()
    {
        // Controleer of de wave al actief is
        if (!waveActive)
        {
            waveActive = true;
            currentWave++;
            Debug.Log("Starting Wave " + currentWave);

            // Voeg hier code toe om de daadwerkelijke wave te starten
            // Bijvoorbeeld: EnemySpawner.Get.StartWave(currentWave);

            // Stel de waarden in voor credits en health
            credits = 200;
            health = 10;

            // Update de labels in het TopMenu met de nieuwe waarden
            topMenu.SetCreditsLabel("Credits: " + credits);
            topMenu.SetHealthLabel("Health: " + health);
            topMenu.SetWaveLabel("Wave: " + currentWave);

            // Start de wave in GameManager
            StartWave();
        }
    }

    public void StartWave()
    {
        // increase the value of currentWave
        currentWave++;

        // change the label for the current wave in topMenu
        topMenu.SetWaveLabel("Wave: " + currentWave);

        // change waveActive to true
        waveActive = true;

        // Start de wave in EnemySpawner
        EnemySpawner.Get.StartWave(currentWave);

        // Reset de enemyInGameCounter naar 0 bij het starten van de wave
        enemyInGameCounter = 0;
    }

    public void EndWave()
    {
        waveActive = false;
        Debug.Log("Ending Wave " + currentWave);

        // Verlaag de counter
        enemyInGameCounter--;

        // Controleer of de wave niet langer actief is en de counter gelijk of kleiner is dan 0
        if (!waveActive && enemyInGameCounter <= 0)
        {
            // Controleer of de huidige wave gelijk is aan de laatste wave
            if (currentWave == 1)
            {
                // Hier zal later de logica voor het einde van de game worden toegevoegd
            }
            else
            {
                // Activeer de wave button in het top menu
                topMenu.EnableWaveButton(true);
            }
        }
    }

    // Functie om de counter met 1 te verhogen wanneer een vijand in het spel komt
    public void AddInGameEnemy()
    {
        enemyInGameCounter++;
    }

    // Functie om de counter te verlagen en de benodigde acties uit te voeren wanneer de vijand uit het spel verdwijnt
    public void RemoveInGameEnemy()
    {
        // Verlaag de counter
        enemyInGameCounter--;

        // Controleer of de wave niet langer actief is en de counter gelijk of kleiner is dan 0
        if (!waveActive && enemyInGameCounter <= 0)
        {
            // Controleer of de huidige wave gelijk is aan de laatste wave
            if (currentWave == 5)
            {
                // Hier zal later de logica voor het einde van de game worden toegevoegd
            }
            else
            {
                // Activeer de wave button in het top menu
                topMenu.EnableWaveButton(true);
            }
        }
    }

    public void SelectSite(ConstructionSite site)
    {
        selectedSite = site;
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

        // Controleer of het SiteLevel overeenkomt met level 0, wat betekent dat een toren wordt verkocht
        if (level == ConstructionSite.SiteLevel.Onbebouwd)
        {
            int sellCost = GetCost(selectedSite.TowerType, selectedSite.Level); // Verkoopkosten ophalen
            AddCredits(sellCost); // Credits toevoegen bij verkoop
            selectedSite.SetTower(null, ConstructionSite.SiteLevel.Onbebouwd, TowerType.Archer); // Geen toren meer op de site
            return;
        }

        // Kosten berekenen op basis van het type en level van de te bouwen toren
        int buildCost = GetCost(type, level);

        // Controleer of er genoeg credits zijn om te bouwen
        if (credits < buildCost)
        {
            Debug.LogWarning("Insufficient credits to build this tower!");
            return;
        }

        // Haal de prefab op basis van het type en level
        List<GameObject> prefabList;
        if (type == TowerType.Archer)
        {
            prefabList = archerPrefabs;
        }
        else if (type == TowerType.Sword)
        {
            prefabList = swordPrefabs;
        }
        else
        {
            prefabList = wizardPrefabs;
        }
        if (prefabList == null)
        {
            Debug.LogWarning("No prefab list found for the selected tower type!");
            return;
        }

        if ((int)level < 0 || (int)level >= prefabList.Count)
        {
            Debug.LogWarning("Invalid level for the selected tower type!");
            return;
        }

        // Bouw de toren en verminder de credits
        GameObject prefab = prefabList[(int)level - 1];
        GameObject tower = Instantiate(prefab, selectedSite.WorldPosition, Quaternion.identity);
        selectedSite.SetTower(tower, level, type);
        RemoveCredits(buildCost); // Verwijder kosten bij bouwen
        towerMenu.SetSite(null); // Verberg het TowerMenu
    }


    private int GetCost(TowerType towerType, ConstructionSite.SiteLevel level, bool v)
    {
        throw new NotImplementedException();
    }

    public void AttackGate()
    {
        health--;
        topMenu.SetHealthLabel("Health: " + health);
    }

    public void AddCredits(int amount)
    {
        credits += amount;
        topMenu.SetCreditsLabel("Credits: " + credits);
        towerMenu.EvaluateMenu();
    }

    public void RemoveCredits(int amount)
    {
        credits -= amount;
        topMenu.SetCreditsLabel("Credits: " + credits);
        towerMenu.EvaluateMenu();
    }

    public int GetCredits()
    {
        return credits;
    }

    public int GetCost(TowerType type, ConstructionSite.SiteLevel level)
    {
        int cost = 0;

        switch (type)
        {
            case TowerType.Archer:
                cost = (level == ConstructionSite.SiteLevel.Level1 ? 100 : (level == ConstructionSite.SiteLevel.Level2 ? 200 : 300));
                break;
            case TowerType.Sword:
                cost = (level == ConstructionSite.SiteLevel.Level1 ? 80 : (level == ConstructionSite.SiteLevel.Level2 ? 160 : 240));
                break;
            case TowerType.Wizard:
                cost = (level == ConstructionSite.SiteLevel.Level1 ? 120 : (level == ConstructionSite.SiteLevel.Level2 ? 240 : 360));
                break;
        }

        return cost;
    }
}
