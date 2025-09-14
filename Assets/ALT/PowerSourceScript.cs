using UnityEngine;
using System.Collections.Generic;

public class PowerSourceScript : MonoBehaviour
{
    #region Verbals
    [SerializeField]
    protected Vector3 startPos = Vector3.zero;              // Starting postion
    [SerializeField]
    protected int helth = 0;                                // Health


    [Header("Bools")]
    [SerializeField]                                        
    private bool isHolding = false;                         // Is holding the power cell
    [SerializeField]
    private bool canTeleport = false;                       // Can teleport 
    [SerializeField]
    private bool inCorectPlace = false;                     // In corect place
    [SerializeField]
    private bool isRoated = false;                          // When the spirte is roated


    [Header("Game object places")]
    [SerializeField]
    private LayerMask toIgnoreLayers;                       // Ignoring layers
    [SerializeField]
    private GameObject corectPlace;                         // Correct Place
    [SerializeField]
    private List<GameObject> teleportObj;                   // Teleport pbjects
    [SerializeField]
    private List<GameObject> canDamgeObj;                   // Teleport layer

    [Header("External")]
    [SerializeField]
    private PowerSourceData powSoData;                      // Power source data
    #endregion

    private void Awake()
    {
        startPos = transform.position;
        helth = powSoData.health;
    }

    #region Detection checks
    // Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Corect place
        if (collision.gameObject.GetInstanceID() == corectPlace.GetInstanceID())
            SetInCorectPlace(true);

        else if (teleportObj != null)
        {
            foreach (var telport in teleportObj)
            {
                if (collision.gameObject.GetInstanceID() == telport.GetInstanceID())
                    // Teleport
                    SetCanTeleport(true);
            }
        }

        if (canDamgeObj != null)
        {
            // Obsticles that do damage
            foreach (var damagOb in canDamgeObj)
            {
                if (collision.gameObject.GetInstanceID() == damagOb.GetInstanceID())
                {
                    SetDamHealth(powSoData.healthDamageFromObj);

                    // Health gone
                    if (GetHealth() <= 0)
                        ResetAll();
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Corect place to set
        if (collision.gameObject.GetInstanceID() == corectPlace.GetInstanceID())
            SetInCorectPlace(false);

        else if (teleportObj != null)
        {
            foreach (var telport in teleportObj)
            {
                if (collision.gameObject.GetInstanceID() == telport.GetInstanceID())
                    // Teleport
                    SetCanTeleport(false);
            }
        }
    }

    #endregion
    public Vector2 StartPos => startPos;

    #region Functionality
    // Get the power source data
    public PowerSourceData GetPowerSourceData()
    {
        return powSoData;
    }


    // Is moving
    public bool GetIsMoving()
    {
        // Get is moving
        return isHolding;
    }
    public void SetIsMoving(bool set_state)
    {
        // Set is moving
         isHolding = set_state;
    }

    // Teleport
    public bool GetCanTeleport()
    {
        // Get can teleport
        return canTeleport;
    }
    public void SetCanTeleport(bool set_state)
    {
        // Set is can teleport
        canTeleport = set_state;
    }

    public bool GetIsRoatedObj()
    {
        return isRoated;
    }


    // In correct place
    public bool GetinCorectPlace()
    {
        // Get in correct place
        return inCorectPlace;
    }
    public void SetInCorectPlace(bool set_state)
    {
        // Set in correct place
        inCorectPlace = set_state;
    }

    // Health
    public int GetHealth()
    {
        // Get Health
        return helth;
    }
    public void SetDamHealth(int dam_amount)
    {
        // Set the health damage
        helth -= dam_amount;

        if (helth < 0)
            helth = 0;
    }
    // Got hit by player's laser
    public void GotHitByPlayer()
    {
        // Set the health damage
        SetDamHealth(powSoData.healthDamageFromPlayer);
    }

    #endregion


    #region Reseters
    public void ResetPos()
    {
        // Reset pos
        transform.position = startPos;
    }

    public void ResetHealth()
    {
        // Reset health
        helth = powSoData.health;
    }

    public void ResetAll()
    {
        // Rest all
        ResetPos();
        ResetHealth();

        // Is moving
        SetIsMoving(false);

        // Can teleport
        SetCanTeleport(false);
        // In corect place
        SetInCorectPlace(false);

        // Box collider 2D is triger = false
        this.GetComponent<BoxCollider2D>().isTrigger = false;

    }
    #endregion
}