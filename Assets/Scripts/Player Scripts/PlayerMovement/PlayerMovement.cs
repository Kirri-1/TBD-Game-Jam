using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(CapsuleCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D m_playerRb;

    [Header("Movement Settings")]
    [SerializeField]
    private bool m_isMoving;
    private Vector3 origPos, targetPos;
    private float m_timeToMove = 0.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_playerRb = GetComponent<Rigidbody2D>();
        DebugHelper.CriticalNullReferenceLogger(this, typeof(Rigidbody2D), "Start()", "GetComponent<Rigidbody2D>()", gameObject, m_playerRb != null);
        //shouldn't activate due to RequireComponent attribute, but just in case
        m_playerRb.gravityScale = 0f;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if (!m_isMoving)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal < 0)
            {
                StartCoroutine(MovePlayer(Vector3.left));
            }
            else if(horizontal > 0)
            {
                StartCoroutine(MovePlayer(Vector3.right));
            }

            if (vertical < 0)
            {
                StartCoroutine(MovePlayer(Vector3.down));
            }
            else if (vertical > 0)
            {
                StartCoroutine(MovePlayer(Vector3.up));
            }



            /*if (Input.GetKey(KeyCode.W))
            {
                StartCoroutine(MovePlayer(Vector3.up));
            }
            if (Input.GetKey(KeyCode.S))
            {
                StartCoroutine(MovePlayer(Vector3.down));
            }
            if (Input.GetKey(KeyCode.A))
            {
                StartCoroutine(MovePlayer(Vector3.left));
            }
            if (Input.GetKey(KeyCode.D))
            {
                StartCoroutine(MovePlayer(Vector3.right));
            }*/
        }
    }
    private IEnumerator MovePlayer(Vector3 direction)
    {
        m_isMoving = true;

        float elapsedTime = 0;

        origPos = transform.position;
        targetPos = origPos + direction;

        while (elapsedTime < m_timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / m_timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        m_isMoving = false;
    }
}
