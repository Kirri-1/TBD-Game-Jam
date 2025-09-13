using System.Linq;
using UnityEngine;

#region RequiredComponents
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
#endregion
public class MoveBullet : MonoBehaviour
{
    #region Variables
    private Rigidbody2D rb;
    [SerializeField] private float speed = 10f;
    private readonly string[] excludedTags = { "Player", "Bullet", "Ground", "Cell" };
    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    void FixedUpdate()
    {
        Vector2 moveDir = transform.up;
        rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (excludedTags.Contains(collision.gameObject.tag))
            return;

        Destroy(gameObject);
    }
}
