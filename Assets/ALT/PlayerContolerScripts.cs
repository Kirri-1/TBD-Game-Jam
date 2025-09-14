using UnityEngine;

public class PlayerContolerScripts : MonoBehaviour
{
    public float moveSpeed = 5;
    public Transform movePoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
