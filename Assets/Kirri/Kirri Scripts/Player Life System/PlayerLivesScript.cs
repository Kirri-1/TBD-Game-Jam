using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLivesScript : MonoBehaviour
{
    #region Variables
    public PlayerLivesSO m_playerLivesSO; //exposed so other scripts can access it
    public int CurrentLives => m_playerLivesSO.currentLives;
    public int MaxLives => m_playerLivesSO.maxLives;
#if UNITY_EDITOR
    public bool resetLivesOnStart = true;
#endif
    #endregion

    #region Start
    private void Start()
    {
        if (m_playerLivesSO == null)
        {
            DebugHelper.CriticalNullReferenceLogger(this, typeof(PlayerLivesSO), "Check the Inspector", "This is in the Inspector, not in the code", gameObject, m_playerLivesSO != null);
            return;
        }
        // Initialize player lives if needed
        if (m_playerLivesSO.currentLives <= 0)
        {
            ResetLives();
        }

#if UNITY_EDITOR
        if(resetLivesOnStart)
            ResetLives();
#endif
    }
    #endregion

    private void Update()
    {
        if (m_playerLivesSO.currentLives != 0)
            return;
        OnDeath();
    }

    #region Lose Life
    public void LoseLife()
    {
        if(m_playerLivesSO == null)
        {
            DebugHelper.CriticalNullReferenceLogger(this, typeof(PlayerLivesSO), "Check the Inspector", "This is in the Inspector, not in the code", gameObject, m_playerLivesSO != null);
            return;
        }

        if (m_playerLivesSO.currentLives > 0)
        {
            m_playerLivesSO.currentLives--;
        }
        else
        {
            DebugHelper.WarningLogger("Player has no more lives", this, "LoseLife() => if(m_playerLivesSO.currentLives > 0)", gameObject);
        }
    }
    #endregion

    #region Gain Life
    public void GainLife(int giveLives = 1)
    {
        if (m_playerLivesSO == null)
        {
            DebugHelper.CriticalNullReferenceLogger(this, typeof(PlayerLivesSO), "Check the Inspector", "This is in the Inspector, not in the code", gameObject, m_playerLivesSO != null);
            return;
        }

        if (m_playerLivesSO.currentLives < m_playerLivesSO.maxLives)
        {
            m_playerLivesSO.currentLives += giveLives;

            m_playerLivesSO.currentLives = Mathf.Min(m_playerLivesSO.currentLives, m_playerLivesSO.maxLives);
        }
        else
        {
            DebugHelper.WarningLogger("Player has maximum lives", this, "if(m_playerLivesSO.currentLives < m_playerLivesSO.maxLives)", gameObject);
        }
    }
    #endregion

    #region Reset Lives
    public void ResetLives()
    {
        if (m_playerLivesSO == null)
        {
            DebugHelper.CriticalNullReferenceLogger(this, typeof(PlayerLivesSO), "Check the Inspector", "This is in the Inspector, not in the code", gameObject, m_playerLivesSO != null);
            return;
        }
        m_playerLivesSO.currentLives = m_playerLivesSO.maxLives;
    }
    #endregion

    #region OnDeath
    private void OnDeath()
    {
        SceneManager.LoadScene("Death Scene", LoadSceneMode.Single);  
    }
    #endregion

    #region OnCollisionEnter2D
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            LoseLife();
        }
        else if (collision.gameObject.CompareTag("Buff"))
        {
            GainLife();
            Destroy(collision.gameObject);
        }
    }
    #endregion
}

