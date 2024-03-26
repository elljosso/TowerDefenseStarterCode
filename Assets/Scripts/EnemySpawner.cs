using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Get { get; private set; }
    public List<GameObject> Path1 = new List<GameObject>();
    public List<GameObject> Path2 = new List<GameObject>();
    public List<GameObject> Enemies = new List<GameObject>();

    private int ufoCounter = 0;

    private void Awake()
    {
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
        ufoCounter = 0;
        switch (number)
        {
            case 1:
                InvokeRepeating("StartWave1", 1f, 1.5f);
                break;
            case 2:
                InvokeRepeating("StartWave2", 1f, 1.5f);
                break;
            case 3:
                InvokeRepeating("StartWave3", 1f, 1.5f);
                break;
            case 4:
                InvokeRepeating("StartWave4", 1f, 1.5f);
                break;
            case 5:
                InvokeRepeating("StartWave5", 1f, 1.5f);
                break;
        }
    }

    private void StartWave1()
    {
        ufoCounter++;
        if (ufoCounter % 6 <= 1)
        {
            return;
        }

        if (ufoCounter < 30)
        {
            SpawnEnemy(0, Path.Path1);
        }
        else
        {
            SpawnEnemy(1, Path.Path1);
        }

        if (ufoCounter > 30)
        {
            CancelInvoke("StartWave1");
            GameManager.Instance.EndWave();
        }
    }

    private void StartWave2()
    {
        ufoCounter++;
        if (ufoCounter % 6 <= 1)
        {
            return;
        }

        if (ufoCounter < 25)
        {
            SpawnEnemy(0, Path.Path1);
        }
        else if (ufoCounter < 35)
        {
            SpawnEnemy(1, Path.Path1);
        }
        else
        {
            SpawnEnemy(2, Path.Path1);
        }

        if (ufoCounter > 35)
        {
            CancelInvoke("StartWave2");
            GameManager.Instance.EndWave();
        }
    }

    private void StartWave3()
    {
        ufoCounter++;
        if (ufoCounter % 6 <= 1)
        {
            return;
        }

        if (ufoCounter < 20)
        {
            SpawnEnemy(0, Path.Path1);
        }
        else if (ufoCounter < 30)
        {
            SpawnEnemy(1, Path.Path1);
        }
        else if (ufoCounter < 40)
        {
            SpawnEnemy(2, Path.Path1);
        }
        else
        {
            SpawnEnemy(3, Path.Path1);
        }

        if (ufoCounter > 40)
        {
            CancelInvoke("StartWave3");
            GameManager.Instance.EndWave();
        }
    }

    private void StartWave4()
    {
        ufoCounter++;
        if (ufoCounter % 6 <= 1)
        {
            return;
        }

        if (ufoCounter < 15)
        {
            SpawnEnemy(0, Path.Path1);
        }
        else if (ufoCounter < 25)
        {
            SpawnEnemy(1, Path.Path1);
        }
        else if (ufoCounter < 35)
        {
            SpawnEnemy(2, Path.Path1);
        }
        else if (ufoCounter < 45)
        {
            SpawnEnemy(3, Path.Path1);
        }
        else
        {
            SpawnEnemy(4, Path.Path1);
        }

        if (ufoCounter > 45)
        {
            CancelInvoke("StartWave4");
            GameManager.Instance.EndWave();
        }
    }

    private void StartWave5()
    {
        ufoCounter++;
        if (ufoCounter % 6 <= 1)
        {
            return;
        }

        if (ufoCounter < 15)
        {
            SpawnEnemy(0, Path.Path1);
            SpawnEnemy(0, Path.Path2);
        }
        else if (ufoCounter < 25)
        {
            SpawnEnemy(1, Path.Path1);
            SpawnEnemy(1, Path.Path2);
        }
        else if (ufoCounter < 35)
        {
            SpawnEnemy(2, Path.Path1);
            SpawnEnemy(2, Path.Path2);
        }
        else if (ufoCounter < 45)
        {
            SpawnEnemy(3, Path.Path1);
            SpawnEnemy(3, Path.Path2);
        }
        else
        {
            SpawnEnemy(4, Path.Path1);
            SpawnEnemy(4, Path.Path2);
        }

        if (ufoCounter > 45)
        {
            CancelInvoke("StartWave5");
            GameManager.Instance.EndWave();
        }
    }

    private void SpawnEnemy(int type, Path path)
    {
        var newEnemy = Instantiate(Enemies[type], Path1[0].transform.position, Path1[0].transform.rotation);
        var script = newEnemy.GetComponentInParent<UFO>();
        if (script != null)
        {
            script.SetPath(path);
            GameObject target = RequestTarget(path, 0);
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
