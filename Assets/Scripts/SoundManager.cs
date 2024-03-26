using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public GameObject audioSourcePrefab;
    public AudioClip[] uiSounds;
    public AudioClip[] towerSounds;

    private void Awake()
    {
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

    public void PlayUISound()
    {
        int index = Random.Range(0, uiSounds.Length);

        // Instantiate an AudioSource GameObject
        GameObject soundGameObject = Instantiate(audioSourcePrefab, transform.position, Quaternion.identity);
        AudioSource audioSource = soundGameObject.GetComponent<AudioSource>();

        // Configure and play the sound
        audioSource.clip = uiSounds[index];
        audioSource.Play();

        // Destroy the AudioSource GameObject after the clip finishes playing
        Destroy(soundGameObject, uiSounds[index].length);
    }

    public void PlayTowerSound(TowerType towerType)
    {
        int index = 0;
        switch (towerType)
        {
            case TowerType.Archer:
                index = 0;
                break;
            case TowerType.Sword:
                index = 1;
                break;
            case TowerType.Wizard:
                index = 2;
                break;
        }

        if (index < towerSounds.Length)
        {
            // Instantiate an AudioSource GameObject
            GameObject soundGameObject = Instantiate(audioSourcePrefab, transform.position, Quaternion.identity);
            AudioSource audioSource = soundGameObject.GetComponent<AudioSource>();

            // Configure and play the sound
            audioSource.clip = towerSounds[index];
            audioSource.Play();

            // Destroy the AudioSource GameObject after the clip finishes playing
            Destroy(soundGameObject, towerSounds[index].length);
        }
        else
        {
            Debug.LogWarning("Tower sound index out of range!");
        }
    }
}
