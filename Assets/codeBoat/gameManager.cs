using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    public float playerMoney = 1000;
    public float fishtSpawnTime;

    public test currentRodScript;
    public RodUpgradeSO rodUpgradeData;
    [SerializeField] private int currentRodLevel = 0;


    private void Start()
    {
        ApplyRodStats();
        UpdateMoneyUI();
        UpdateShopUI();

        upgradeMenu.SetActive(false);
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

    //=================== SHOP SYSTEM N UI ===================//

    [SerializeField] private GameObject upgradeMenu;
    [SerializeField] private TextMeshProUGUI playerCountMoney;
    [SerializeField] private TextMeshProUGUI nextUpgradeText;
    [SerializeField] private Button upgradeButton;


    public void UpdateMoneyUI()
    {
        if (playerCountMoney != null)
            playerCountMoney.text = "$" + playerMoney.ToString("0");
    }

    public void ToggleShop()
    {
        bool active = !upgradeMenu.activeSelf; 

        if (active)
        {
            upgradeMenu.SetActive(active);
            UpdateShopUI();
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


   

    public void UpgradeRod_()
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

            UpdateMoneyUI();
            UpdateShopUI();

            Debug.Log("Upgrade Success!");
        }
        else
        {
            Debug.Log("Not enough money!");
        }

    }
}
