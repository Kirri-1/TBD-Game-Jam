using UnityEngine;
using System.Collections.Generic;

public class PowerSourceManger : MonoBehaviour
{
    [SerializeField]
    private int ObjListCount = 0;

    [SerializeField]
    protected List<GameObject> powerSourceOb = new();

    void Start()
    {
        ObjListCount = powerSourceOb.Count;
    }

    void Update()
    {
        
    }
}
