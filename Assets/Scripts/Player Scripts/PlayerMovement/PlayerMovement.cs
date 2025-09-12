using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(CapsuleCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D m_playerRb;

    [Header("Movement Settings")]
    [SerializeField]
    float m_moveSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_playerRb = GetComponent<Rigidbody2D>();
        DebugHelper.CriticalNullReferenceChecker(this, typeof(Rigidbody2D), "Start()", "GetComponent<Rigidbody2D>()", gameObject, m_playerRb != null);
        //shouldn't activate due to RequireComponent attribute, but just in case
        m_playerRb.gravityScale = 0f;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical).normalized * m_moveSpeed;

        m_playerRb.linearVelocity = new Vector2(movement.x, movement.y);
    }
}
