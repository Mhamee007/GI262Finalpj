using UnityEngine;

[CreateAssetMenu(fileName = "FishType", menuName = "Fishing/Fish Type")]
public class fishType : ScriptableObject
{
        public string fishName;
        public float speed = 1f;
        public float awarenessRadius = 5f;
        public float biteChance = 1f;
        public float escapeChance = 0.2f;
        public int price = 20;
        public int exp = 1;
}
    
