using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections;

public class Play : MonoBehaviour
{
    Button button;
    string thisScene = "Main Menu";
    Scene nextScene;
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        button = root.Q<Button>("Play");

        if (button != null)
            button.clicked += OnButtonClicked;
        else
            Debug.LogError("Button 'Play' not found in UXML!");
    }

    private void OnButtonClicked()
    {
        StartCoroutine(UnloadMainScene());
    }

    private IEnumerator UnloadMainScene()
    {
        AsyncOperation loadDevScene = SceneManager.LoadSceneAsync("Kirri Dev Scene", LoadSceneMode.Additive);
        yield return loadDevScene;


        nextScene = SceneManager.GetSceneByName("Level 1");
        SceneManager.SetActiveScene(nextScene);

        AsyncOperation loadPauseScene = SceneManager.LoadSceneAsync("Pause", LoadSceneMode.Additive);
        yield return loadPauseScene;

        Scene scene = SceneManager.GetSceneByName("Pause");
        if (scene.isLoaded)
        {
            foreach (var rootObj in scene.GetRootGameObjects())
            {
                rootObj.SetActive(false);
            }
        }


        AsyncOperation unloadMenuScreen = SceneManager.UnloadSceneAsync(thisScene);
        yield return unloadMenuScreen;
    }
}
