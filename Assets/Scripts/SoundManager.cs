using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource menuMusic;
    public AudioSource gameMusic;

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

    public void StartMenuMusic()
    {
        if (gameMusic.isPlaying)
            gameMusic.Stop();

        if (!menuMusic.isPlaying)
            menuMusic.Play();
    }

    public void StartGameMusic()
    {
        if (menuMusic.isPlaying)
            menuMusic.Stop();

        if (!gameMusic.isPlaying)
            gameMusic.Play();
    }
}