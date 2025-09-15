using UnityEngine;

public class PlayerContolerScripts : MonoBehaviour
{
    public bool isMoving = false;
    public bool isHoldingCell = false;

    public float moveSpeed = 5;
    public Transform movePoint;
    public float gridOffset = 0.5f;

    [Space(10)]
    public LayerMask colidables;
    public LayerMask pickableCell;

    [Space(10)]
    public MovingDir MovingDirEnms = MovingDir.up;
    public KeyCode pickUpPowerCellKey = KeyCode.Space;

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

        // X dir
        if (xInput <= -1)
            MovingDirEnms = MovingDir.left;

        else if (xInput >= 1)
            MovingDirEnms = MovingDir.right;

        // Y dir
        else if (yInput <= -1)
            MovingDirEnms = MovingDir.down;

        else if (yInput >= 1)
            MovingDirEnms = MovingDir.up;


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

            if (Mathf.Abs(yInput) == 1f)
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