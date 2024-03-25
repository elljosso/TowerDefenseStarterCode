using UnityEngine;
using UnityEngine.UIElements;

public class TopMenu : MonoBehaviour
{
    private Button startWaveButton;
    private Label waveLabel;
    private Label creditsLabel;
    private Label gateHealthLabel;

    private int currentWave = 1;
    private int credits = 100;
    private int gateHealth = 100;
    private Button startGameButton;

    void Start()
    {
        // Krijg toegang tot de root van het UI-document
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Zoek de button en labels op basis van hun naam
        startWaveButton = root.Q<Button>("startWaveButton");
        if (startWaveButton == null) Debug.Log("startwavebutton not found");
        waveLabel = root.Q<Label>("waveLabel");
        if (waveLabel == null) Debug.Log("wavelabel not found");
        creditsLabel = root.Q<Label>("creditsLabel");
        if (creditsLabel == null) Debug.Log(" creditslabel not found");
        gateHealthLabel = root.Q<Label>("gateHealthLabel");
        if (gateHealthLabel == null) Debug.Log("gatehealthlabel not found");

        startWaveButton.clicked += StartWave; // Voeg de StartWave-methode toe als een event handler voor de startWaveButton-klik

        // Update de UI met de initiële waarden
        UpdateUI();
    }

    void OnDestroy()
    {
        startWaveButton.clicked -= StartWave;
    }

    // Functie om een nieuwe wave te starten
    void StartWave()
    {
        Debug.Log("Starting Wave " + currentWave);

        // Simuleer enkele acties die plaatsvinden tijdens een wave
        currentWave++;
        credits += 10;
        gateHealth -= 10;

        // Update de UI met de nieuwe waarden
        UpdateUI();

        // Start de wave in GameManager
        GameManager.Instance.StartWave(); // Dit start de wave in GameManager
        EnableWaveButton(false); // Deactiveer de wave knop wanneer een wave is gestart
    }

    // Functie om de UI bij te werken met de huidige waarden
    void UpdateUI()
    {
        // Update de tekstlabels met de huidige waarden
        waveLabel.text = "Wave: " + currentWave.ToString();
        creditsLabel.text = "Credits: " + credits.ToString();
        gateHealthLabel.text = "Gate Health: " + gateHealth.ToString();

        // Schakel de startWaveButton uit als de gateHealth 0 of minder is
        startWaveButton.SetEnabled(gateHealth > 0);
    }

    // Functie om de wave label aan te passen
    public void SetWaveLabel(string text)
    {
        waveLabel.text = text;
    }

    // Functie om de credits label aan te passen
    public void SetCreditsLabel(string text)
    {
        creditsLabel.text = text;
    }

    // Functie om de gate health label aan te passen
    public void SetHealthLabel(string text)
    {
        gateHealthLabel.text = text;
    }

    // Functie om de wave knop in of uit te schakelen
    public void EnableWaveButton(bool enabled)
    {
        startWaveButton.SetEnabled(enabled);
    }
}