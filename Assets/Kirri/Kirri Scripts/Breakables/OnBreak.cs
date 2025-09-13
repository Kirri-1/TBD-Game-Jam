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
    [SerializeField]
    private List<string> m_excludedTagsList = new List<string> { "Player", "Ground", "Wall", "Bullet", "Cell" };

    private HashSet<string> m_excludedTags;


    [SerializeField]
    float m_timer = 0.5f;
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_excludedTags = new HashSet<string>(m_excludedTagsList);
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
        if (collider.gameObject.CompareTag("Cell"))
        {
            DebugHelper.InfoLogger("Trigger initiated, object has the tag \"Cell \"", this, "HandleTrigger() => collider.CompareTag(\"Cell\")", collider.gameObject, true);

            PowerSourceScript powerSource = collider.gameObject.GetComponent<PowerSourceScript>();
            if (powerSource == null)
            {
                DebugHelper.CriticalNullReferenceLogger(this, typeof(PowerSourceScript), "HandleTrigger() =>", "PowerSourceScript powerSource = collider.GetComponent<PowerSourceScript>();",
                    collider.gameObject, powerSource != null);
                return;
            }
            powerSource.ResetAll();

            return;
        }
    }
    #endregion

    #region SetInactive

    private IEnumerator SetInactive(float time, Collider2D collider)
    {
        yield return new WaitForSeconds(time);
        collider.gameObject.SetActive(false);
    }
    #endregion
}
