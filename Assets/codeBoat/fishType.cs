using UnityEngine;

[CreateAssetMenu(fileName = "FishType", menuName = "Fishing/Fish Type")]
public class fishType : ScriptableObject
{
    public string fishName;
    public float speed = 1f;
    public float awarenessRadius = 2f;
    public float biteChance = 0.6f;     
    public float escapeChance = 0.2f;   
    public int price = 20;
}
