using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLivesSO", menuName = "Scriptable Objects/PlayerLivesSO")]
public class PlayerLivesSO : ScriptableObject
{
    public int currentLives = 4;
    public int maxLives = 4; //may be changed later
}
