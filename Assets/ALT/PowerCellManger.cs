using UnityEngine;
using System.Collections.Generic;

public class PowerCellManger : MonoBehaviour
{
    [SerializeField]
    private List<PowerCellScript> powerCellOb = new();

    [SerializeField]
    private List<PowerCellInfo> powerCellInfo = new();


    private void Awake()
    {
        for(int i = 0; i < powerCellOb.Count; i++)
        {
            powerCellOb[i].SetPowerCellInfo(powerCellInfo[i]);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            powerCellOb[0].TeleportPowerCell();
    }

    // Getters
    public List<PowerCellScript> GetPowerCellScript => powerCellOb;
    public List<PowerCellInfo> GetPowerCellInfo => powerCellInfo;
}
