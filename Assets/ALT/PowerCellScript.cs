using UnityEngine;
using System.Collections.Generic;

public class PowerCellScript : MonoBehaviour
{
    #region Verbals
    [SerializeField]
    protected Vector3 startPos = Vector3.zero;              // Starting postion
    [SerializeField]
    protected int helth = 0;                                // Health
    // Sprites
    [SerializeField]
    private Sprite normalSpr = null, roatedSpr = null;       // Current sprite and roated sprites


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
    private GameObject corectPlace = null;                  // Correct Place

    [SerializeField]
    private List<GameObject> teleportObj = new();           // Teleport objects
    [SerializeField]
    private int teleportObjColID = 0;                       // Teleport object collied ID

    [SerializeField]
    private List<GameObject> canDamgeObj = new();           // Object that do damge

    [Header("External")]
    [SerializeField]
    private PowerCellData powSoData = null;                 // Power cell data
    #endregion

    private void Awake()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = normalSpr;

        startPos = transform.position;
        helth = powSoData.health;
    }

    private void Start()
    {
        this.gameObject.GetComponent<BoxCollider2D>().excludeLayers = toIgnoreLayers;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TeleportPowerCell();
    }


    #region Detection checks
    // Triggers
    private void OnTriggerEnter2D(Collider2D coll)
    {
        // Corect place
        if (coll.gameObject.GetInstanceID() == corectPlace.GetInstanceID())
            inCorectPlace = true;

        else if (teleportObj.Count > 0)
        {
            for (var i = 0; i < teleportObj.Count; i++)
            {
                if (coll.gameObject.GetInstanceID() == teleportObj[i].GetInstanceID())
                {
                    // Get transp obj ID
                    teleportObjColID = teleportObj[i].GetInstanceID();

                    // Teleport
                    canTeleport = true;
                    break;
                }
            }
        }

        if (canDamgeObj.Count > 0)
        {
            // Obsticles that do damage
            foreach (var damagOb in canDamgeObj)
            {
                if (coll.gameObject.GetInstanceID() == damagOb.GetInstanceID())
                {
                    SetDamHealth(powSoData.healthDamageFromObj);

                    // Health gone
                    if (helth <= 0)
                        ResetAll();

                    break;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        // Corect place to set
        if (coll.gameObject.GetInstanceID() == corectPlace.GetInstanceID())
            inCorectPlace = false;

        else if (teleportObj.Count > 0)
        {
            for (var i = 0; i < teleportObj.Count; i++)
            {
                if (coll.gameObject.GetInstanceID() == teleportObj[i].GetInstanceID())
                {
                    // Reset transp obj ID
                    teleportObjColID = 0;

                    // Teleport
                    canTeleport = false;

                    break;
                }
            }
        }
    }

    #endregion


    // Get the power cell data
    public PowerCellData GetPowerSourceData => powSoData;


    #region Get set
    // Start postion
    public Vector2 StartPos => startPos;

    // Set is holding
    public bool GetIsHolding { get { return isHolding; } set { isHolding = value; } }

    // In correct place get
    public bool GetInCorectPlace { get { return inCorectPlace; }}
    #endregion


    #region Teleport function
    // Teleport set : get
    public bool GetCanTeleport { get { return canTeleport; } set { canTeleport = value; } }

    // Is roatated
    public bool GetIsRoatedObj => isRoated;

    // Telaport the cell
    public void TeleportPowerCell()
    {
        if (canTeleport)
        {
            for (var i = 0; i < teleportObj.Count; i++)
            {
                // Check ID
                if (teleportObjColID == teleportObj[i].GetInstanceID())
                {
                    int array_point =  i+1;

                    // Check if not max length
                    if (i >= teleportObj.Count - 1)
                        array_point = 0;

                    // Set pos
                    this.transform.position = teleportObj[array_point].transform.position;

                    // Change sprite
                    Sprite set_sprite = roatedSpr;

                    // Is sprite is roated
                    if (!isRoated && this.GetComponent<SpriteRenderer>().sprite == normalSpr)
                        isRoated = true;
                    else
                    {
                        isRoated = false;
                        set_sprite = normalSpr;
                    }


                    // Change sprite
                    this.GetComponent<SpriteRenderer>().sprite = set_sprite;

                    break;
                }
            }
        }
    }

    #endregion


    #region Health
    // Health
    public int GetHealth => helth;

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
        GetIsHolding = false;

        // Can teleport
        GetCanTeleport = false;
        // In corect place
        inCorectPlace = false;
    }
    #endregion
}