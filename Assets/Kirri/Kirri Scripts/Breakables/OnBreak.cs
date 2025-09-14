using UnityEngine;
using System.Collections.Generic;
using System.Collections;

#region RequiredComponents
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(BoxCollider2D))]
#endregion
public class OnBreak : MonoBehaviour
{
    #region Variables

    private HashSet<string> m_excludedTags;

    [SerializeField]
    private BulletGameManager m_bulletManager;

    [SerializeField]
    float m_timer = 0.5f;
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_bulletManager = GameObject.Find("Bullet Game Manager(Clone)").GetComponent<BulletGameManager>();
        if (m_bulletManager == null)
        {
            DebugHelper.CriticalNullReferenceLogger(this, typeof(BulletGameManager), "Start() =>",
                "m_bulletManager = GameObject.Find(\"Bullet Game Manager(Clone)\").GetComponent<BulletGameManager>();", gameObject, m_bulletManager != null);
            return;
        }
        if (m_bulletManager.m_excludedTagsBreakables == null || m_bulletManager.m_excludedTagsBreakables.Count == 0)
        {
            DebugHelper.CriticalLogger("Excluded tags list is null!", this, "if (m_bulletManager.m_excludedTagsBreakables == null || m_bulletManager.m_excludedTagsBreakables.Count == 0)", gameObject);
            return;
        }
        m_excludedTags = new HashSet<string>(m_bulletManager.m_excludedTagsBreakables);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleTrigger(collision);
    }

    #region Handle Trigger
    void HandleTrigger(Collider2D collider)
    {
        DebugHelper.InfoLogger("Trigger initiated", this, "HandleTrigger()", collider.gameObject, true);
        if (m_excludedTags.Contains(collider.gameObject.tag))
        {
            DebugHelper.InfoLogger("Trigger initiated, object had an excluded tag", this, "HandleTrigger() => m_excludedTags.Contains(collider.gameObject.tag"
                , collider.gameObject, true);
            return;
        }
       

        DebugHelper.InfoLogger("Trigger initiated, object has a tag that allows for setting it inactive", this, "HandleTrigger() => collider.CompareTag(\"Cell\")", collider.gameObject, true);
        StartCoroutine(SetInactive(m_timer, collider));
    }
    #endregion

    #region OnCollisionEnter2D
    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (!collider.gameObject.CompareTag("Cell"))
            return;


        DebugHelper.InfoLogger("Trigger initiated, object has the tag \"Cell \"", this, "HandleTrigger() => collider.CompareTag(\"Cell\")", collider.gameObject, true);

        PowerSourceScript powerSource = collider.gameObject.GetComponent<PowerSourceScript>();

        Vector2 currentPos = powerSource.gameObject.transform.position;

        if (currentPos == powerSource.StartPos)
        {
            Destroy(gameObject);
            return;
        }

        if (powerSource == null)
        {
            DebugHelper.CriticalNullReferenceLogger(this, typeof(PowerSourceScript), "HandleTrigger() =>", "PowerSourceScript powerSource = collider.GetComponent<PowerSourceScript>();",
                collider.gameObject, powerSource != null);
            return;
        }
        powerSource.ResetAll();
        Destroy(gameObject);

        return;
    }
    #endregion

    #region SetInactive

    private IEnumerator SetInactive(float time, Collider2D collider)
    {
        yield return new WaitForSeconds(time);
        collider.gameObject.SetActive(false);
        Destroy(gameObject);
    }
    #endregion
}
