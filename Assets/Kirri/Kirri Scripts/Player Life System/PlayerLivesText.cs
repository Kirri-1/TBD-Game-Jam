using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PlayerLivesText : MonoBehaviour
{
    #region Variables
    private TextMeshProUGUI m_playerText;
    private PlayerLivesScript m_playerLivesScript;
    #endregion

    #region Start
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_playerText = GetComponent<TextMeshProUGUI>();

        if(m_playerText == null)
        {
            DebugHelper.CriticalNullReferenceLogger(this, typeof(TextMeshProUGUI), "Ensure there is a TextMeshProUGUI component on this GameObject", "This is on this GameObject, not in the code", gameObject, m_playerText != null);
            return;
        }

        m_playerLivesScript = GetComponentInParent<PlayerLivesScript>();
        if(m_playerLivesScript == null)
        {
            DebugHelper.CriticalNullReferenceLogger(this, typeof(PlayerLivesScript), "Ensure there is a PlayerLivesScript component in the parent GameObjects", "This is in the parent GameObjects, not in the code", gameObject, m_playerLivesScript != null);
            return;
        }
    }
    #endregion

    #region Update
    // Update is called once per frame
    void Update()
    {
        if (m_playerLivesScript == null)
        {
            DebugHelper.CriticalNullReferenceLogger(this, typeof(PlayerLivesScript), "Ensure there is a PlayerLivesScript component in the parent GameObjects", "This is in the parent GameObjects, not in the code", gameObject, m_playerLivesScript != null);
            return;
        }

        string currentLives = $"Current Lives: {m_playerLivesScript.CurrentLives.ToString()}";
        m_playerText.text = currentLives;
    }
    #endregion
}
