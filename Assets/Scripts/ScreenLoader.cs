using UnityEngine;

public class ScreenLoader : MonoBehaviour
{
    public bool MenuScene;

    private void Start()
    {
        SoundManager soundManager = SoundManager.Instance;
        if (soundManager != null)
        {
            if (MenuScene)
            {
                //soundManager.StartMenuMusic();
            }
            else
            {
                //soundManager.StartGameMusic();
            }
        }
        else
        {
            Debug.LogWarning("SoundManager is not initialized!");
        }
    }
}
