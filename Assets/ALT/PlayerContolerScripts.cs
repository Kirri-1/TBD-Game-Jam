using UnityEngine;

public class PlayerContolerScripts : MonoBehaviour
{
    [SerializeField]
    private bool isMoving = false;
    [SerializeField]
    private Animator animator;
    public bool isHoldingCell = false;


    public float moveSpeed = 5;
    public Transform movePoint;
    public float gridOffset = 0.5f;

    [Space(10)]
    public LayerMask colidables;
    private int LastFaceDirection = 0;

    [Space(10)]
    public MovingDir MovingDirEnms = MovingDir.up;
    public KeyCode pickUpPowerCellKey = KeyCode.Space;
    public Rigidbody2D rb2D = null;

    void Start()
    {
        movePoint.parent = null;
        movePoint.position = this.transform.position;
        rb2D = GetComponent<Rigidbody2D>();
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

        // Add extra space for power cell
        int addCellWith = 0;
        if (isHoldingCell)
            addCellWith = 1;
        // Move the postion of the player
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        // If player has moved enough away from the start to end point 
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {

            if (Mathf.Abs(xInput) == 1f)
            {
                if (xInput < 0)
                    addCellWith = -addCellWith;

                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(xInput + addCellWith, 0f, 0f), .2f, colidables))
                    movePoint.position += new Vector3(xInput, 0f, 0f);
            }

            else if (Mathf.Abs(yInput) == 1f)
            {
                if (yInput < 0)
                    addCellWith = -addCellWith;

                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, yInput + addCellWith, 0f), .2f, colidables))
                    movePoint.position += new Vector3(0f, yInput, 0f);
            }
        }


        if (Input.GetKey(pickUpPowerCellKey))
            isHoldingCell = true;

        else if (Input.GetKeyUp(pickUpPowerCellKey))
            isHoldingCell = false;
    }
}