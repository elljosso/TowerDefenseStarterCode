using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class HighScoreMenu : MonoBehaviour
{
    public Label GameOver; // Link naar het eerste label in de UI
    public Label HighScore; // Link naar het tweede label in de UI
    public Button PlayAgain; // Link naar de knop in de UI

    private Button playAgainButton;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Zoek de PlayAgain button en het textField op basis van hun naam
        playAgainButton = root.Q<Button>("PlayAgain");
        playAgainButton.SetEnabled(false);
        // Controleer of HighScoreManager bestaat
        if (HighScoreManager.Instance != null)
        {
            // Vraag de waarde van GameIsWon op
            bool gameWon = HighScoreManager.Instance.GameIsWon;

            // Pas de tekst van het eerste label aan op basis van de waarde van GameIsWon
            if (gameWon)
            {
                GameOver.text = "Congratulations! You Won!";
            }
            else
            {
                GameOver.text = "Game Over! You Lost!";
            }
        }

        // Voeg een callbackfunctie toe aan de PlayAgain knop
        PlayAgain.clicked += OnPlayAgainButtonClicked;
    }

    // Callbackfunctie voor wanneer de PlayAgain knop wordt geklikt
    void OnPlayAgainButtonClicked()
    {
        // Laad de GameScene
        SceneManager.LoadScene("GameScene");
    }
}
