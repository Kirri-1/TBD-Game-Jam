using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float xInput = 0.0f;
    [SerializeField]
    private float yInput = 0.0f;

    // Componets
    private Rigidbody2D rBody2D = null;


    void Start()
    {
        rBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int speed = 20;

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        if (xInput != 0 || yInput != 0)
        {
            rBody2D.MovePosition(new Vector2(transform.position.x + xInput * speed * Time.deltaTime,
                transform.position.y + yInput * speed * Time.deltaTime));
        }
    }
}
