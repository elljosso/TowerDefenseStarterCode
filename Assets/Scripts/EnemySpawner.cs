using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Singleton deel
    public static EnemySpawner Get { get; private set; }
    public List<GameObject> Path1 = new List<GameObject>();
    public List<GameObject> Path2 = new List<GameObject>();
    public List<GameObject> Enemies = new List<GameObject>();


    private int ufoCounter = 0; // Counter voor het bijhouden van het aantal UFO's

    private void Awake()
    {
        // Singleton-patroon: zorg ervoor dat er slechts één instantie van EnemySpawner is
        if (Get != null && Get != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Get = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void StartWave(int number)
    {
        // Reset de counter
        ufoCounter = 0;

        switch (number)
        {
            case 1:
                // Start de eerste wave met een interval van 1.5 seconden
                InvokeRepeating("StartWave1", 1f, 1.5f);
                break;
                // Voeg hier andere wave-cases toe en implementeer de bijbehorende StartWave methoden
        }
    }

    private void StartWave1()
    {
        ufoCounter++; // Verhoog de counter

        // Laat enkele pauzes tussen de UFO's
        if (ufoCounter % 6 <= 1)
        {
            return;
        }

        // Laat verschillende UFO's spawnen gedurende de wave
        if (ufoCounter < 30)
        {
            SpawnEnemy(0, Path.Path1); // Spawn een standaard UFO
        }
        else
        {
            SpawnEnemy(1, Path.Path1); // Spawn een verbeterde UFO
        }

        // Beëindig de wave na een bepaald aantal UFO's
        if (ufoCounter > 30)
        {
            CancelInvoke("StartWave1"); // Stop het herhaaldelijk aanroepen van StartWave1
            GameManager.Instance.EndWave(); // Laat de GameManager weten dat de wave is geëindigd
        }
    }

    private void SpawnEnemy(int type, Path path)
    {
        // Instantieer een nieuwe vijand (UFO)
        var newEnemy = Instantiate(Enemies[type], Path1[0].transform.position, Path1[0].transform.rotation);

        var script = newEnemy.GetComponentInParent<UFO>();

        if (script != null)
        {
            // Stel het pad en het doel van de vijand in
            script.SetPath(path);
            GameObject target = RequestTarget(path, 0); // Begin bij het eerste waypoint
            script.SetTarget(target);
        }
    }

    public GameObject RequestTarget(Path path, int index)
    {
        if (path == Path.Path1)
        {
            if (index < Path1.Count - 1)
            {
                index++;
                return Path1[index];
            }
            else
            {
                return null;
            }
        }
        else if (path == Path.Path2)
        {
            if (index < Path2.Count - 1)
            {
                index++;
                return Path2[index];
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}
