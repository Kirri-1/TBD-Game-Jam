using System.Collections.Generic;
using UnityEngine;

public class BulletGameManager : MonoBehaviour
{
    public static BulletGameManager Instance { get; private set; }

    [SerializeField] private BulletGameManager prefab;

    public List<string> m_excludedTagsBreakables = new List<string>();

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

    public static BulletGameManager EnsureExists(BulletGameManager prefabRef = null)
    {
        if (Instance == null)
        {
            if (prefabRef != null)
            {
                Instance = Instantiate(prefabRef);
                DontDestroyOnLoad(Instance.gameObject);
            }
            else
            {
                GameObject go = new GameObject("BulletGameManager");
                Instance = go.AddComponent<BulletGameManager>();
                DontDestroyOnLoad(go);
            }
        }
        return Instance;
    }
}
