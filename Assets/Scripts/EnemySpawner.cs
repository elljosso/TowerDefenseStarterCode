using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Singleton part
    public static EnemySpawner Get { get; private set; }
    public List<GameObject> Path1 = new List<GameObject>();
    public List<GameObject> Path2 = new List<GameObject>();
    public List<GameObject> Enemies = new List<GameObject>();

    private void SpawnEnemy(int type, Path path)

    {

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
    public void SpawnTester()
    {

        SpawnEnemy(0, Path.Path1);

    }
    public void Start()
    {
        InvokeRepeating("SpawnTester", 1f, 1f);
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

    private void Awake()
    {
        if (Get != null && Get != this)
            Destroy(this);
        else
        {
            Get = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
