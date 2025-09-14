using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PowerCellInfo
{
    // Current sprite and rotateed sprites

    [Header("Sprites")]
    [SerializeField]
    private Sprite normalSpr = null;
    [SerializeField]
    private Sprite roatedSpr = null;


    [Space(5)]
    [Header("Power Cell Class")]
    [SerializeField]
    private PowerCellData powerCellData;

    [Space(5)]
    [Header("Interactble")]
    [SerializeField]
    private LayerMask toIgnoreLayers;                       // Ignoring layers

    [SerializeField]
    private GameObject corectPlace = null;                  // Correct Place

    [SerializeField]
    private List<GameObject> canGetDamgeObj = new();           // Object that do damge

    [SerializeField]
    private List<GameObject> teleportObj = new();           // Teleport objects

    // Getters
    public Sprite GetNormalSpr { get { return normalSpr; } set { normalSpr = value; } }
    public Sprite GetRoatedSpr { get { return roatedSpr; } set { roatedSpr = value; } }
    public PowerCellData GetPowerCellData { get { return powerCellData; } set { powerCellData = value; } }
    public LayerMask GetLayerToIgnore { get { return toIgnoreLayers; } set { toIgnoreLayers = value; } }
    public GameObject GetCorectPlace { get { return corectPlace; } set { corectPlace = value; } }
    public List<GameObject> GetCanGetDamgeObj { get { return canGetDamgeObj; } set { canGetDamgeObj = value; } }
    public List<GameObject> GetTeleportObjs { get { return teleportObj; } set { teleportObj = value; } }
}
