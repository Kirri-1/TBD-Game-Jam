using System.Collections;
using UnityEngine;

public class SlidingOnIce : MonoBehaviour //scrapped
{
    /*PlayerMovement m_playerMovement;
    private Vector2 m_slideDirection;
    Rigidbody2D m_playerRb;
    [SerializeField] private float m_iceSpeed = 3f;
    private bool m_coroutineIsStarted = false;
    private bool m_isSliding => m_playerMovement.isSliding;
    private bool m_isContacted = false;

    void Start()
    {
        m_playerMovement = GetComponent<PlayerMovement>();
        m_playerRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (m_isSliding && m_slideDirection == Vector2.zero)
            CaptureDirection();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ice") || collision.gameObject.CompareTag("Bullet"))
            return;
        

        if (m_playerMovement.isSliding == true)
        {
            m_playerMovement.isSliding = false;
            m_isContacted = true;
            return;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ice") || collision.gameObject.CompareTag("Bullet"))
            return;

        m_isContacted = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Ice"))
        {
            m_playerMovement.isSliding = false;
        }

        m_playerMovement.isSliding = true;

        if (m_slideDirection == Vector2.zero)
            m_slideDirection = m_playerMovement.LastMovementDirection;

        if (!m_coroutineIsStarted)
        {
            StartCoroutine(SlideRoutine());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Ice")) return;

        m_playerMovement.isSliding = false;
        m_slideDirection = Vector2.zero;
    }

    private void CaptureDirection()
    {
        var keyCode = KeyCode.A;

        switch (keyCode)
        {
            case KeyCode.A:
                m_slideDirection = Vector2.left;
                break;

            case KeyCode.D:
                m_slideDirection = Vector2.right;
                break;

            case KeyCode.W:
                m_slideDirection = Vector2.up;
                break;

            case KeyCode.S:
                m_slideDirection = Vector2.down;
                break;
        }
    }

    private IEnumerator SlideRoutine()
    {
        m_coroutineIsStarted = true;
        while (m_isSliding && m_slideDirection != Vector2.zero && !m_isContacted)
        {
            Vector2 newPos = m_playerRb.position + m_slideDirection * m_iceSpeed * Time.fixedDeltaTime;
            m_playerRb.MovePosition(newPos);

            yield return new WaitForFixedUpdate();
        }
        m_playerMovement.isSliding = false;
        m_coroutineIsStarted = false;
    }*/
}
