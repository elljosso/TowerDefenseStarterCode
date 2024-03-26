using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class IntroMenu : MonoBehaviour
{
    private Button playButton;
    private Button quitButton;
    private TextField nameField;
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Zoek de buttons en het textField op basis van hun naam
        playButton = root.Q<Button>("PlayButton");
        quitButton = root.Q<Button>("QuitButton");
        nameField = root.Q<TextField>("NameField");

        // Voeg callbackfuncties toe voor knopklikken
        playButton.clicked += OnPlayButtonClicked;

        // Voeg een directe callback toe voor de Quit Button zonder tussenfunctie
        quitButton.clicked += Application.Quit;

        // Schakel de Start Button uit wanneer het menu wordt geopend
        playButton.SetEnabled(false);

        // Voeg een callback toe voor wanneer de tekst in het TextField verandert
        if (nameField != null)
        {
            nameField.RegisterValueChangedCallback(evt =>
            {
                // Wanneer de naam verandert, update de PlayerName property in HighScoreManager
                HighScoreManager.Instance.PlayerName = evt.newValue;
                OnNameValueChanged(evt.newValue);
            });
        }
    }

    void OnDestroy()
    {
        // Verwijder de callbacks om geheugenlekken te voorkomen
        playButton.clicked -= OnPlayButtonClicked;
    }

    // Callbackfunctie voor wanneer de Play Button wordt geklikt
    void OnPlayButtonClicked()
    {
        // Laad de GameScene
        SceneManager.LoadScene("GameScene");
    }

    // Callbackfunctie voor wanneer de waarde van het TextField verandert
    void OnNameValueChanged(string newName)
    {
        // Controleer of de ingevoerde naam minstens 3 tekens lang is
        if (newName.Length >= 3)
        {
            // Maak de Start Button actief als de naam minstens 3 tekens lang is
            playButton.SetEnabled(true);
        }
        else
        {
            // Schakel de Start Button uit als de naam minder dan 3 tekens lang is
            playButton.SetEnabled(false);
        }
    }
}
