using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private GameObject m_destination;
    [SerializeField]
    private bool isTeleporting;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
            return;

        collision.transform.position = m_destination.transform.position;
        collision.transform.rotation = m_destination.transform.rotation;
    }
}

