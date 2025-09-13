using UnityEngine;
using System.Collections.Generic;

public class PowerSourceScript : MonoBehaviour
{
    #region Verbals
    [SerializeField]
    protected Vector3 startPos = Vector3.zero;              // Starting postion
    [SerializeField]
    protected int helth = 0;                                // Health


    [Header("Actions")]
    [SerializeField]                                        
    private bool isMoving = false;                          // Is moving
    [SerializeField]
    private bool canTeleport = false;                       // Can teleport 
    [SerializeField]
    private bool inCorectPlace = false;                     // In corect place

    [Header("Layeres")]
    [SerializeField]
    private LayerMask toIgnoreLayers;                       // Ignoring layers
    [SerializeField]
    private LayerMask corectPlaceLayer;                     // Correct Place layer
    [SerializeField]
    private LayerMask teleportLayer;                        // Teleport layer
    [SerializeField]
    private LayerMask obDamgeLayers;                        // Teleport layer

    [Header("External")]
    [SerializeField]
    private PowerSourceData powSoData;                      // Power source data


    #endregion

    private void Awake()
    {
        startPos = transform.position;
        helth = powSoData.health;
    }

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, toIgnoreLayers.value);
    }


    #region Detection checks
    // Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == teleportLayer)
            // Teleport
            SetCanTeleport(true);



        // Corect place to set
        if (collision.gameObject.layer == corectPlaceLayer)
            SetInCorectPlace(true);


        // Obsticles that do damage
        if (collision.gameObject.layer == obDamgeLayers)
        {
            SetDamHealth(powSoData.healthDamageFromObj);

            Debug.Log("Got hit by: " + obDamgeLayers.ToString());

            // Health gone
            if (GetHealth() <= 0)
                ResetAll();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Teleport
        if (collision.gameObject.layer == teleportLayer)
            SetCanTeleport(false);


        // Corect place to set
        if (collision.gameObject.layer == corectPlaceLayer)
            SetInCorectPlace(false);
    }

    #endregion


    #region Functionality
    // Is moving
    public bool GetIsMoving()
    {
        // Get is moving
        return isMoving;
    }
    public void SetIsMoving(bool set_state)
    {
        // Set is moving
         isMoving = set_state;
    }

    // Can teleport
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