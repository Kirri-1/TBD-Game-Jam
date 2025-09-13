using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class OnBreak : MonoBehaviour
{
    [SerializeField]
    private float m_waitTime = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;

        StartCoroutine(WaitForAnimation(m_waitTime));
    }

    private IEnumerator WaitForAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
