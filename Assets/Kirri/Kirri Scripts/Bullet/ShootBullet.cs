using System.Collections;
using UnityEngine;


public class ShootBullet : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject m_bulletPrefabs; // 0=Right, 1=Left, 2=Up, 3=Down

    [SerializeField] private Transform m_bulletSpawn;
    [SerializeField]
    private bool m_canShoot = true;
    [SerializeField]
    private float timer = 0.5f;
    #endregion

    private void Start()
    {
        m_canShoot = true;
    }

    void Update()
    {
        if (m_canShoot)
        {
            HandleInput();
        }
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
            StartCoroutine(ReactivateShoot(timer));
        }
    }
    #endregion
    private IEnumerator ReactivateShoot(float timer)
    {
        m_canShoot = false;
        yield return new WaitForSeconds(timer);
        m_canShoot = true;
    }
}
