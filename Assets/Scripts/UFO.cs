using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    public float speed = 1f;
    public float health = 10f;
    public int points = 1;
    public Path path { get; set; }
    public GameObject target { get; set; }
    private int pathIndex = 1;

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);

        // Controleer hoe dichtbij we bij het doel zijn
        if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
        {
            // Als we dichtbij zijn, vraag om een nieuw waypoint
            target = EnemySpawner.Get.RequestTarget(path, pathIndex);
            pathIndex++;

            // Als het doel null is, hebben we het einde van het pad bereikt.
            // Vernietig de vijand op dit punt
            if (target == null)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetPath(Path newPath)
    {
        path = newPath;
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    public void Damage(int damage)
    {
        // Verminder de gezondheidswaarde
        health -= damage;

        // Als de gezondheid kleiner of gelijk is aan nul
        // Vernietig het gameobject
        if (health <= 0)
        {
            // Voeg hier eventueel punten toe aan de speler of iets anders
            Destroy(gameObject);
        }
    }
}