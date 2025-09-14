using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    HashSet<string> dontPause = new HashSet<string>() { "Play", "Pause", "Quit", "QuitPause", "Resume" };
    public static List<MonoBehaviour> pausedScripts = new List<MonoBehaviour>();
    public static bool isPaused = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            PauseGame();
            isPaused = true;
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;

        foreach (var script in FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None))
        {
            if (dontPause.Contains(script.GetType().Name) || script == this)
                continue;

            script.enabled = false;
            pausedScripts.Add(script);
        }
        Scene scene = SceneManager.GetSceneByName("Pause");
        if (scene.isLoaded)
        {
            foreach (var rootObj in scene.GetRootGameObjects())
            {
                rootObj.SetActive(true);
            }
        }
    }
}
