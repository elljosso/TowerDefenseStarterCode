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

    private ConstructionSite selectedSite; // Variabele om de geselecteerde bouwplaats te onthouden

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
    }

    public void SelectSite(ConstructionSite site)
    {
        // Onthoud de geselecteerde bouwplaats
        selectedSite = site;

        // Roep de SetSite-methode aan in het TowerMenu-script en geef de geselecteerde bouwplaats door
        towerMenu.SetSite(selectedSite);
    }
}