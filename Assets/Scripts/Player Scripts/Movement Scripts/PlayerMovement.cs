using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float m_moveSpeed = 5f;
    Rigidbody2D m_playerRb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_playerRb = GetComponent<Rigidbody2D>();
        DebugHelper.CriticalNullReferenceChecker(this, typeof(Rigidbody2D), "Start()", "m_playerRb = GetComponent<Rigidbody2D>();", gameObject, m_playerRb != null);
        //should never be null because of the RequireComponent attribute. But just in case.
        m_playerRb.gravityScale = 0f;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput * m_moveSpeed, verticalInput * m_moveSpeed);

        m_playerRb.linearVelocity = movement;
    }
}
