using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Resume : MonoBehaviour
{
    private Button button;

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape))
            return;

        ResumeGame();
    }

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        button = root.Q<Button>("Resume");

        if (button != null)
            button.clicked += ResumeGame;
        else
            Debug.LogError("Button 'Resume' not found in UXML!");
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;

        foreach (var script in Pause.pausedScripts)
        {
            if (script != null)
                script.enabled = true;
        }


        Scene pauseScene = SceneManager.GetSceneByName("Pause");
        if (pauseScene.isLoaded)
        {
            foreach (var rootObj in pauseScene.GetRootGameObjects())
            {
                rootObj.SetActive(false);
            }
        }
        Pause.isPaused = false;
    }
}
