using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    public float playerMoney = 1000;
    public float fishtSpawnTime;

    public test currentRodScript;
    public RodUpgradeSO rodUpgradeData;
    [SerializeField] private int currentRodLevel = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
       
    }

    private void Start()
    {
        ApplyRodStats();
    }

    public void UpgradeRod()
    {
      
        if (currentRodLevel + 1 >= rodUpgradeData.upgrades.Length)
        {
            Debug.Log("Max Level Reached!");
            return;
        }

   
        var nextUpgrade = rodUpgradeData.upgrades[currentRodLevel + 1];

     
        if (playerMoney >= nextUpgrade.upgradePrice)
        {
         
            playerMoney -= nextUpgrade.upgradePrice;

            
            currentRodLevel++;

          
            ApplyRodStats();

            Debug.Log($"Upgrade Success! Level: {currentRodLevel}, Money Left: {playerMoney}");
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    private void ApplyRodStats()
    {
       if (currentRodScript != null && rodUpgradeData != null)
       {
      
          if (currentRodLevel < rodUpgradeData.upgrades.Length)
          {
            var data = rodUpgradeData.upgrades[currentRodLevel];

            currentRodScript.SetRodStats(data);
            
            Debug.Log("Complayr change!");
          }
       }
    }
}
