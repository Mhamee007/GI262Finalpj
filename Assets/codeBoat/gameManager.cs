using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    public int playerMoney = 100;
    public int playerExp = 0;

    public float fishtSpawnTime;

    public rod currentRodScript;
    public RodUpgradeSO rodUpgradeData;
    [SerializeField] private int currentRodLevel = 0;

    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ApplyRodStats();
        UpdateMoneyUI();
        UpdateShopUI();
        UpdateLevelUI();

        if (upgradeMenu != null)
        { 
            upgradeMenu.SetActive(false); 
        }
    }
  
    public void UpgradeRod()
    {
      
        if (currentRodLevel + 1 >= rodUpgradeData.upgrades.Length)
        {
            Debug.Log("Max Level Reached!");
            return;
        }
   
        var nextUpgrade = rodUpgradeData.upgrades[currentRodLevel + 1];

        if (playerMoney >= nextUpgrade.upgradePrice && playerExp >= nextUpgrade.level)
        {
            playerMoney -= nextUpgrade.upgradePrice;
            currentRodLevel++;
          
            ApplyRodStats();
            UpdateMoneyUI();
            UpdateShopUI();

            Debug.Log($"Upgrade Success! Level: {currentRodLevel}, Money Left: {playerMoney}");
        }
        else if (playerMoney < nextUpgrade.upgradePrice)
        {
            Debug.Log("Not enough money!");
        }
        else
        {
            Debug.Log("Not enough Level!");
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

    //=================== FISH SYSTEM ===================//
    public void AddMoney(int amount)
    {
        playerMoney += amount;
        Debug.Log("Money: " + playerMoney);
    }

    public void AddExp(int amount)
    {
        playerExp += amount;
        Debug.Log("EXP: " + playerExp);
    }

    //=================== SHOP SYSTEM N UI ===================//

    [SerializeField] private GameObject upgradeMenu;
    [SerializeField] private TextMeshProUGUI playerCountMoney;
    [SerializeField] private TextMeshProUGUI playerCurrentLevel;
    [SerializeField] private TextMeshProUGUI nextUpgradeText;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button openShopButton;


    public void UpdateMoneyUI()
    {
        if (playerCountMoney != null)
            playerCountMoney.text = "$" + playerMoney.ToString("0");
    }

    public void UpdateLevelUI()
    {
        if (playerCurrentLevel != null)
            playerCurrentLevel.text = "Level:" + playerExp.ToString("0");
    }

    public void ToggleShop()
    {
        if (upgradeMenu != null)
        {
            bool isActive = !upgradeMenu.activeSelf;
            upgradeMenu.SetActive(isActive);

            if (isActive)
            {
                UpdateShopUI();
            }
        }
    }

    public void UpdateShopUI()
    {
        if (currentRodLevel + 1 >= rodUpgradeData.upgrades.Length)
        {
            nextUpgradeText.text = "Max Level!";
            upgradeButton.interactable = false;
            return;
        }

        var next = rodUpgradeData.upgrades[currentRodLevel + 1];

        nextUpgradeText.text = "Upgrade Price: $" + next.upgradePrice;
        upgradeButton.interactable = playerMoney >= next.upgradePrice;
    }
}
