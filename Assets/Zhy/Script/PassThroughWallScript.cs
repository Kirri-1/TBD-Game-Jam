using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mono.Cecil.Cil;
public class PassThroughWallScript : MonoBehaviour
{
    [SerializeField]
    private List<PowerCellScript> powerCells = new();
    [SerializeField]
    private List<GameObject> cellObject = new();
    [SerializeField]
    private CanPassThroughWall passThrough = CanPassThroughWall.blue;


    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var cells in powerCells)
        {
            if (cells.GetCanGoThroughWall != passThrough)
            {
                // cells.ResetPos();
                Debug.Log("Erro");
            }
        }
    }
}


public enum CanPassThroughWall
{
    red = 0,
    white = 1,
    blue = 2,
    green = 3
}