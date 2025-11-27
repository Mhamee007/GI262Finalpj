using UnityEngine;

[CreateAssetMenu(fileName = "FishType", menuName = "Fishing/Fish Type")]
public class fishType : ScriptableObject
{        
        public float speed = 1f;
        public float awarenessRadius = 3f;
        public float biteChance = 25f;
        public float escapeChance = 0.2f;
        public int price = 20;
        public int exp = 1;
}
    
