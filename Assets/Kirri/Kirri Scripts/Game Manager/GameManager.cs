using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static string currentScene;
    private HashSet<string> ignoreScenes = new HashSet<string> { "Death Scene", "Main Menu", "Pause" };
    [SerializeField]
    private BulletGameManager m_bulletManager;

    [SerializeField]
    private List<string> allScenes = new List<string>();

    public static List<string> AllScenes;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        StartCoroutine(WaitForGameToLoad());
        InstantiateAll();
    }

    private void Update()
    {
        string activeScene = SceneManager.GetActiveScene().name;
        if (activeScene == currentScene || ignoreScenes.Contains(activeScene))
            return;
        currentScene = SceneManager.GetActiveScene().name;
    }

    private void InstantiateAll()
    {
        var bulletManger = InstantiateWithoutClone(m_bulletManager);
    }

    private IEnumerator WaitForGameToLoad(float time = 2f)
    {
        yield return new WaitForSeconds(time);

        AllScenes = new List<string>(allScenes);
    }

    private T InstantiateWithoutClone<T>(T prefab) where T : MonoBehaviour
    {
        if (prefab == null) return null;

        // Instantiate the prefab
        T instance = Instantiate(prefab);

        // Remove the "(Clone)" suffix
        instance.gameObject.name = prefab.gameObject.name;

        // Optional: persist across scenes
        DontDestroyOnLoad(instance.gameObject);

        return instance;
    }
}
