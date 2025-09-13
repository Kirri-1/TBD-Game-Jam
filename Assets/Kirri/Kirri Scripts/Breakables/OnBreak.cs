using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class OnBreak : MonoBehaviour
{
    [SerializeField]
    private float m_waitTime = 0.5f;

    public List<string> m_excludedTagsList = new List<string>{ "Player", "Wall", "Ground", "Laser", "Cell", "Teleporter", "CorrectPanel", "Bullet" }; 
    //TODO: change this to pull from a manager list

    private HashSet<string> m_excludedTags;

    private void Awake()
    {
        m_excludedTags = new HashSet<string>(m_excludedTagsList);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_excludedTags.Contains(collision.gameObject.tag))
            return;

        StartCoroutine(WaitForAnimation(m_waitTime, collision));
    }

    private IEnumerator WaitForAnimation(float time, Collider2D collider)
    {
        yield return new WaitForSeconds(time);
        collider.gameObject.SetActive(false);
    }
}
