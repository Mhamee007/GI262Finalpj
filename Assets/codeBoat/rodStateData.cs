using UnityEngine;

[CreateAssetMenu(fileName = "NewRodUpgrade", menuName = "Fishing/RodUpgradeData")]
public class RodUpgradeSO : ScriptableObject
{
    [System.Serializable]
    public class RodUpgradeData 
    {
        public int level;
        public float maxLength;
        public float reelSpeed;
        public float throwPowerMultiplier;
        public int upgradePrice;
    }
    public RodUpgradeData[] upgrades;
}