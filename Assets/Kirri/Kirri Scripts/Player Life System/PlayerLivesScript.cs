using UnityEngine;

public class PlayerLivesScript : MonoBehaviour
{
    public PlayerLivesSO m_playerLivesSO; //exposed so other scripts can access it
    public int CurrentLives => m_playerLivesSO.currentLives;
    public int MaxLives => m_playerLivesSO.maxLives;

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
    }

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

    public void ResetLives()
    {
        if (m_playerLivesSO == null)
        {
            DebugHelper.CriticalNullReferenceLogger(this, typeof(PlayerLivesSO), "Check the Inspector", "This is in the Inspector, not in the code", gameObject, m_playerLivesSO != null);
            return;
        }
        m_playerLivesSO.currentLives = m_playerLivesSO.maxLives;
    }
}
