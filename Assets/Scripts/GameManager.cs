using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject TowerMenu; // GameObject referentie naar het TowerMenu
    private TowerMenu towerMenu; // Script referentie naar het TowerMenu

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
}