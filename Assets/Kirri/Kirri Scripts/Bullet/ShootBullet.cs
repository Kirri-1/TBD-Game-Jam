using UnityEngine;


public class ShootBullet : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject m_bulletPrefabs; // 0=Right, 1=Left, 2=Up, 3=Down

    [SerializeField] private Transform m_bulletSpawn;
    #endregion

    void Update()
    {
        HandleInput();
    }

    #region HandleInput
    void HandleInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h != 0) v = 0;

        // If no input, default to right
        if (h == 0 && v == 0)
            h = 1;

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(m_bulletPrefabs, m_bulletSpawn.position, m_bulletSpawn.rotation);
        }
    }
    #endregion
}
