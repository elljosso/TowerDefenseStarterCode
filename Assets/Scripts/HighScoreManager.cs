using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HighScoreManager : MonoBehaviour
{
    // Singleton instantie
    public static HighScoreManager Instance { get; private set; }

    // Public properties
    public string PlayerName { get; set; }
    public bool GameIsWon { get; set; }

    // Class om high scores bij te houden
    [Serializable]
    public class HighScore
    {
        public string Name;
        public int Score;
    }

    // Lijst met high scores
    public List<HighScore> HighScores = new List<HighScore>();

    // Het pad naar het JSON-bestand waarin de high scores worden opgeslagen
    private string filePath;

    // Awake wordt aangeroepen wanneer het script wordt geladen
    private void Awake()
    {
        // Controleer of er al een instantie van de klasse bestaat
        if (Instance == null)
        {
            // Zo niet, maak deze instantie de singleton
            Instance = this;
            // Zorg ervoor dat dit object niet wordt vernietigd wanneer de scène verandert
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Als er al een instantie bestaat, vernietig deze instantie
            Destroy(gameObject);
        }

        // Het pad naar het JSON-bestand
        filePath = Application.persistentDataPath + "/highscores.json";

        // Kijk of het JSON-bestand bestaat
        if (File.Exists(filePath))
        {
            // Laad de high scores vanuit het JSON-bestand
            LoadHighScores();
        }
    }

    // Methode om een high score toe te voegen
    public void AddHighScore(int score)
    {
        // Kijk of deze score hoger is dan minstens 1 score in de lijst
        bool scoreAdded = false;
        for (int i = 0; i < HighScores.Count; i++)
        {
            if (score > HighScores[i].Score)
            {
                // Voeg een nieuwe high score toe op de juiste positie
                HighScores.Insert(i, new HighScore { Name = PlayerName, Score = score });
                scoreAdded = true;
                break;
            }
        }

        // Als de score niet hoger is dan bestaande scores, voeg deze dan toe aan het einde van de lijst
        if (!scoreAdded)
        {
            HighScores.Add(new HighScore { Name = PlayerName, Score = score });
        }

        // Sorteer de lijst volgens score, van hoog naar laag
        HighScores.Sort((x, y) => y.Score.CompareTo(x.Score));

        // Verwijder het laatste element als de lijst meer dan 5 elementen bevat
        if (HighScores.Count > 5)
        {
            HighScores.RemoveAt(HighScores.Count - 1);
        }

        // Schrijf de high scores naar het JSON-bestand
        SaveHighScores();
    }

    // Methode om high scores naar een JSON-bestand te schrijven
    private void SaveHighScores()
    {
        string json = JsonUtility.ToJson(HighScores);
        File.WriteAllText(filePath, json);
    }

    // Methode om high scores vanuit een JSON-bestand te laden
    private void LoadHighScores()
    {
        string json = File.ReadAllText(filePath);
        HighScores = JsonUtility.FromJson<List<HighScore>>(json);
    }
}
