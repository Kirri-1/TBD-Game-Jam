using UnityEngine;

public class RotateSpawnBullet : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private Transform[] m_spawnTransforms;
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HandleRotation();
    }

    #region HandleRotation
    void HandleRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey("left"))
        {
            transform.position = m_spawnTransforms[0].position;
            transform.rotation = m_spawnTransforms[0].rotation;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey("right"))
        {
            transform.position = m_spawnTransforms[1].position;
            transform.rotation = m_spawnTransforms[1].rotation;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey("up"))
        {
            transform.position = m_spawnTransforms[2].position;
            transform.rotation = m_spawnTransforms[2].rotation;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey("down"))
        {
            transform.position = m_spawnTransforms[3].position;
            transform.rotation = m_spawnTransforms[3].rotation;
        }
    }
    #endregion
}
