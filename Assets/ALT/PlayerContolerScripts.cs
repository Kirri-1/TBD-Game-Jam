using UnityEngine;

public class PlayerContolerScripts : MonoBehaviour
{
    [SerializeField]
    private bool isMoving = false;
    [SerializeField]
    private Animator animator;


    public float moveSpeed = 5;
    public Transform movePoint;
    public float gridOffset = 0.5f;
    public LayerMask colidables;
    private int LastFaceDirection = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movePoint.parent = null;
        movePoint.position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // get input
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", xInput);
        animator.SetFloat("Vertical", yInput);
        animator.SetFloat("Speed", new Vector2(xInput, yInput).sqrMagnitude);
        
        if (xInput != 0 || yInput != 0)
        {
            if (Mathf.Abs(xInput) == 1f)
            {
                LastFaceDirection = (int)xInput;
            }
            if (Mathf.Abs(yInput) == 1f)
            {
                LastFaceDirection = (int)yInput * 2;
            }
        }
        animator.SetFloat("LastFaceDirection", LastFaceDirection);

        // Move the postion of the player
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        // If player has moved enough away from the start to end point 
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            if (Mathf.Abs(xInput) == 1f)
            {
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(xInput, 0f, 0f), .2f, colidables))
                    movePoint.position += new Vector3(xInput, 0f, 0f);
            }

            if (Mathf.Abs(yInput) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, yInput, 0f), .2f, colidables))

                    movePoint.position += new Vector3(0f, yInput, 0f);
            }
        }
    }
}
