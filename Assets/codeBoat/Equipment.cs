using System;
using UnityEngine;

public enum FishingState1 { Idle, Charging, Casted, Reeling }

public class Equipment : gameManager
{
    [SerializeField] protected RodUpgradeData[] upgrades;
    public Rigidbody2D bobber;
    public float playerLvRequired;
    public FishingState1 state = FishingState1.Idle;

    public float throwPowerMultiplier;
    public float reelSpeed = 0.1f;
    public float maxLength = 10f;
    public int currentLevel = 0;

   [System.Serializable]
    public class RodUpgradeData
    {
        public int level;
        public float maxLength;
        public float reelSpeed;
        public float throwPowerMultiplier;
        public int upgradePrice;
    }

    public virtual void Description()
    {
        Console.WriteLine("Basic rod");
    }

    public void ApplyUpgradeStats()
    {
        if (currentLevel < upgrades.Length)
        {
            maxLength = upgrades[currentLevel].maxLength;
            reelSpeed = upgrades[currentLevel].reelSpeed;
        }
    }

    public bool TryUpgrade(gameManager gm)
    {
        if (currentLevel >= upgrades.Length - 1)
        {
            Debug.Log("Rod is max level.");
            return false;
        }

        RodUpgradeData next = upgrades[currentLevel + 1];

        int requiredPlayerLv = (currentLevel + 1) * 5;

        if (gm.playerLvl < requiredPlayerLv)
        {
            Debug.Log($"Need player level {requiredPlayerLv} to upgrade rod.");
            return false;
        }

        if (gm.playerMoney >= next.upgradePrice)
        {
            gm.playerMoney -= next.upgradePrice;
            currentLevel++;

            ApplyUpgradeStats();
            Debug.Log($"Rod upgraded to Lv.{currentLevel}");

            return true;
        }
        else
        {
            Debug.Log("Not enough money.");
            return false;
        }
    }
}


