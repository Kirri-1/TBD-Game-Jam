using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DeathResume : MonoBehaviour
{
    Button button;
    private string deathScene = "Death";
    private string currentScene;
    private void Start()
    {
        currentScene = GameManager.currentScene;
    }
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        button = root.Q<Button>("Resume");

        if (button != null)
            button.clicked += OnButtonClicked;
        else
            Debug.LogError("Button 'Quit' not found in UXML!");
    }
    private void OnButtonClicked()
    {
        StartCoroutine(ReloadScene());
    }
    private IEnumerator ReloadScene()
    {
        SceneManager.LoadSceneAsync(currentScene, LoadSceneMode.Additive);
        yield return new WaitForSeconds(1.0f);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene));

        AsyncOperation loadPause = SceneManager.LoadSceneAsync("Pause", LoadSceneMode.Additive);
        yield return loadPause;

        Scene scene = SceneManager.GetSceneByName("Pause");
        if (scene.isLoaded)
        {
            foreach (var rootObj in scene.GetRootGameObjects())
            {
                rootObj.SetActive(false);
            }
        }


        AsyncOperation unloadDeathScene =  SceneManager.UnloadSceneAsync(deathScene);
        yield return unloadDeathScene;

    }
}
