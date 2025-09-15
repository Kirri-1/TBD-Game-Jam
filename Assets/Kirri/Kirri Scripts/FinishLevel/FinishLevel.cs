using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    private bool levelCompleted = false;
    private List<PowerCellScript> powerCellScripts = new List<PowerCellScript>();
    string thisScene = SceneManager.GetActiveScene().name;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(var powerCell in FindObjectsByType<PowerCellScript>(FindObjectsSortMode.None))
        {
            if (powerCellScripts.Contains(powerCell))
                continue;
            powerCellScripts.Add(powerCell); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!levelCompleted)
        {
            FinishLevels();
        }
    }

    void FinishLevels()
    {

        foreach (var correctPlace in powerCellScripts)
        {

            if (!correctPlace.inCorectPlace)
            {
                Debug.Log($"Not in correct place: {correctPlace.name}");
                return;
            }
        }



        switch (thisScene)
        {
            case "Level 1":
                StartScene("Level 2");
                break;

            case "Level 2":
                StartScene("Level 3");
                break;

            case "Level 3":
                SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
                Debug.Log("yo");
                break;
        }
        levelCompleted = true;
    }

    void StartScene(string sceneName)
    {
        StartCoroutine(NextScene(sceneName));
    }

    private IEnumerator NextScene(string sceneName)
    {
        string thisSceneName = SceneManager.GetActiveScene().name;

        AsyncOperation loadScene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return loadScene;

        Scene scene = SceneManager.GetSceneByName(sceneName);

        SceneManager.SetActiveScene(scene);

        AsyncOperation unloadScene = SceneManager.UnloadSceneAsync(thisSceneName);
        yield return unloadScene;
    }
}
