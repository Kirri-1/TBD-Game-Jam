using UnityEngine;

[CreateAssetMenu(fileName = "PowerSourceData", menuName = "Scriptable Objects/helth")]
public class PowerSourceData : ScriptableObject
{
    public int health = 0;
    public int healthDamageFromObj = 0;
    public int healthDamageFromPlayer = 0;
    public bool telpoRotate = false;
}
