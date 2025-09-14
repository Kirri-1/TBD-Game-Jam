using System.Collections;
using UnityEngine;

#region RequiredComponents
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(BoxCollider2D))]
#endregion
public class PlayerMovement : MonoBehaviour
{
    #region Variables
    Rigidbody2D m_playerRb;

    [Header("Movement Settings")]
    [SerializeField]
    private bool m_isMoving;
    private Vector3 origPos, targetPos;
    private float m_timeToMove = 0.2f;
    [SerializeField]
    private LayerMask m_collision;
    #endregion

    #region Start
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_playerRb = GetComponent<Rigidbody2D>();
        DebugHelper.CriticalNullReferenceLogger(this, typeof(Rigidbody2D), "Start() =>", "GetComponent<Rigidbody2D>()", gameObject, m_playerRb != null);
        //shouldn't activate due to RequireComponent attribute, but just in case
        m_playerRb.gravityScale = 0f;
    }
    #endregion

    private void FixedUpdate()
    {
        Movement();
    }

    #region Movement
    void Movement()
    {
        if (m_isMoving)
            return;

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            if (horizontal != 0)
            {
                StartCoroutine(MovePlayer(new Vector2(horizontal, 0)));
            }
            else if (vertical != 0)
            {
                StartCoroutine(MovePlayer(new Vector2(0, vertical)));
            }
    }
    #endregion

    #region Move Player Timer
    private IEnumerator MovePlayer(Vector2 direction)
    {
        m_isMoving = true;

        Vector2 startPos = m_playerRb.position;
        Vector2 targetPos = startPos + direction;

        RaycastHit2D hit = Physics2D.Raycast(startPos, direction, 1f, m_collision);
        if (hit.collider != null)
        {
            m_isMoving = false;
            yield break;
        }

        float elapsedTime = 0f;
        while (elapsedTime < m_timeToMove)
        {
            Vector2 newPos = Vector2.Lerp(startPos, targetPos, elapsedTime / m_timeToMove);
            m_playerRb.MovePosition(newPos);
            elapsedTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        m_playerRb.MovePosition(targetPos);

        m_isMoving = false;
    }
    #endregion
}
