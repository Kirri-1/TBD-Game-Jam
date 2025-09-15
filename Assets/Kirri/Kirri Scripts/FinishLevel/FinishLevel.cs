#region Namespaces
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
#endregion

public class FinishLevel : MonoBehaviour
{
    #region Variables
    private bool levelCompleted = false;
    private List<PowerCellScript> powerCellScripts = new List<PowerCellScript>();
    string thisScene;
    private bool startUpdate;
    private List<bool> m_finished = new List<bool>();
    #endregion
    [SerializeField]
    private List<string> m_scenes;

    #region Start
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (powerCellScripts.Count > 0)
        {
            powerCellScripts.Clear();
        }
        m_scenes = new List<string>(GameManager.AllScenes);


        startUpdate = false;
        StartCoroutine(GetCells());
    }
    #endregion

    #region Update
    // Update is called once per frame
    void Update()
    {
        HandleUpdate();
    }
    #endregion

    #region HandleUpdate
    void HandleUpdate()
    {
        if (!startUpdate || levelCompleted)
            return;

        for (int i = 0; i < powerCellScripts.Count; i++)
        {
            m_finished[i] = powerCellScripts[i].inCorectPlace;
        }

        if (m_finished.All(f => f))
        {
            levelCompleted = true;
            ImprovedSwitchScene();
        }
    }
    #endregion

    void ImprovedSwitchScene()
    {

        thisScene = SceneManager.GetActiveScene().name;
        int listLength = m_scenes.Count;

        int index = m_scenes.IndexOf(thisScene);
        string nextScene = (index >= 0 && index + 1 < m_scenes.Count) ? m_scenes[index + 1] : "Main Menu";

        StartScene(nextScene);
    }


    #region SwitchScene
    void SwitchScene()
    {
        thisScene = SceneManager.GetActiveScene().name;
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
        return; //gonna be doing some experimenting so doing this now so nothing breaks during it
    }
    #endregion

    #region StartScene
    void StartScene(string sceneName)
    {
        StartCoroutine(NextScene(sceneName));
    }
    #endregion

    #region NextScene
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
    #endregion

    #region GetCells
    private IEnumerator GetCells(float waitSeconds = 1)
    {
        yield return new WaitForSeconds(waitSeconds);
        foreach (var powerCell in FindObjectsByType<PowerCellScript>(FindObjectsSortMode.None))
        {
            if (powerCellScripts.Contains(powerCell))
                continue;
            powerCellScripts.Add(powerCell);
        }
        startUpdate = true;
        m_finished = new List<bool>(new bool[powerCellScripts.Count]);
    }
    #endregion
}
