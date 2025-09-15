using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PowerCellScript : MonoBehaviour
{
    #region Verbals
    [SerializeField]
    private Vector3 startPos = Vector3.zero;                // Starting postion
    [SerializeField]
    private int helth = 0;                                  // Health

    [Header("Bools")]
    [SerializeField]                                        
    private bool isHolding = false;                         // Is holding the power cell
    [SerializeField]
    private bool canTeleport = false;                       // Can teleport 
    [SerializeField]
    private bool inCorectPlace = false;                     // In corect place
    [SerializeField]
    private bool isRoated = false;                          // When the spirte is roated

    [Space(10)]
    [SerializeField]
    private int teleportObjColID = 0;                       // Teleport object collied ID

    // Power cell info
    [Space(10)]
    [SerializeField]
    private PowerCellInfo powerInfo = new();
    #endregion


    public void SetPowerCellInfo(PowerCellInfo PowInfo)
    {
        powerInfo = PowInfo;

        startPos = transform.position;
        helth = powerInfo.GetPowerCellData.health;

        this.gameObject.GetComponent<SpriteRenderer>().sprite = powerInfo.GetNormalSpr;
    }

    // Get the power cell data
    public PowerCellData GetPowerSourceData => powerInfo.GetPowerCellData;


    #region Detection checks
    // Triggers
    private void OnTriggerEnter2D(Collider2D coll)
    {
        // Corect place
        if (coll.gameObject.GetInstanceID() == powerInfo.GetCorectPlace.GetInstanceID())
            inCorectPlace = true;

        else if (powerInfo.GetTeleportObjs.Count > 0)
        {
            for (var i = 0; i < powerInfo.GetTeleportObjs.Count; i++)
            {
                if (coll.gameObject.GetInstanceID() == powerInfo.GetTeleportObjs[i].GetInstanceID())
                {
                    // Get transp obj ID
                    teleportObjColID = powerInfo.GetTeleportObjs[i].GetInstanceID();

                    // Teleport
                    canTeleport = true;
                    break;
                }
            }
        }

        if (powerInfo.GetCanGetDamgeObj.Count > 0)
        {
            // Obsticles that do damage
            foreach (var damagOb in powerInfo.GetCanGetDamgeObj)
            {
                if (coll.gameObject.GetInstanceID() == damagOb.GetInstanceID())
                {
                    SetDamHealth(powerInfo.GetPowerCellData.healthDamageFromObj);

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
        if (coll.gameObject.GetInstanceID() == powerInfo.GetCorectPlace.GetInstanceID())
            inCorectPlace = false;

        else if (powerInfo.GetTeleportObjs.Count > 0)
        {
            for (var i = 0; i < powerInfo.GetTeleportObjs.Count; i++)
            {
                if (coll.gameObject.GetInstanceID() == powerInfo.GetTeleportObjs[i].GetInstanceID())
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


    #region Get set
    // Set is holding
    public bool GetIsHolding { get { return isHolding; } set { isHolding = value; } }

    // Start postion
    public Vector2 StartPos => startPos;

    // In correct place get
    public bool GetInCorectPlace { get { return inCorectPlace; }}
    #endregion

    #region Holding
    public void HoldPowerCell(Vector3 playerPos, MovingDir movingDir)
    {
        isHolding = true;
        Vector3 setPos = Vector3.zero;

        // Up
        if (movingDir == MovingDir.up)
            setPos = new Vector3(playerPos.x, playerPos.y + this.transform.localScale.y, 0);
        //Down
        else if (movingDir == MovingDir.down)
            setPos = new Vector3(playerPos.x, playerPos.y - this.transform.localScale.y, 0);
        // Left
        else if (movingDir == MovingDir.left)
            setPos = new Vector3(playerPos.x - this.transform.localScale.x, playerPos.y, 0);
        // Right
        else if (movingDir == MovingDir.right)
            setPos = new Vector3(playerPos.x + this.transform.localScale.x, playerPos.y, 0);

        transform.position = setPos;
    }

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
            for (var i = 0; i < powerInfo.GetTeleportObjs.Count; i++)
            {
                // Check ID
                if (teleportObjColID == powerInfo.GetTeleportObjs[i].GetInstanceID())
                {
                    int array_point =  i+1;

                    // Check if not max length
                    if (i >= powerInfo.GetTeleportObjs.Count - 1)
                        array_point = 0;

                    // Set pos
                    this.transform.position = powerInfo.GetTeleportObjs[array_point].transform.position;


                    if (powerInfo.GetPowerCellData.telpoRotate)
                    {
                        // Change sprite
                        Sprite set_sprite = powerInfo.GetRoatedSpr;

                        // Is sprite is roated
                        if (!isRoated && this.GetComponent<SpriteRenderer>().sprite == powerInfo.GetNormalSpr)
                            isRoated = true;
                        else
                        {
                            isRoated = false;
                            set_sprite = powerInfo.GetNormalSpr;
                        }

                        // Change sprite
                        this.GetComponent<SpriteRenderer>().sprite = set_sprite;
                    }

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
        SetDamHealth(powerInfo.GetPowerCellData.healthDamageFromPlayer);
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
        helth = powerInfo.GetPowerCellData.health;
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

public enum MovingDir
{
    up = 0,
    down = 1,
    left = 2,
    right = 3
}