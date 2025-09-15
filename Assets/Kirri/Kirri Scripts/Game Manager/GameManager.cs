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
        BulletGameManager.EnsureExists(m_bulletManager);
    }
}
