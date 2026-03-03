using UnityEngine;
using TMPro; // Needed for UI

public class Business : MonoBehaviour
{
    [Header("Business Setup")]
    public BusinessData data;
    public int currentLevel = 1;

    [Header("UI Elements")]
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI costText;

    // Internal trackers
    private float productionTimer = 0f;
    private bool isProducing = false;

    void Start()
    {
        UpdateUI(); // Set the initial text when the game starts
    }

    void Update()
    {
        if (isProducing)
        {
            productionTimer += Time.deltaTime;
            if (productionTimer >= data.productionTime)
            {
                CollectRevenue();
            }
        }
    }

    public void StartProduction()
    {
        if (!isProducing)
        {
            isProducing = true;
            productionTimer = 0f;
        }
    }

    private void CollectRevenue()
    {
        isProducing = false;
        productionTimer = 0f;

        double moneyEarned = data.baseRevenue * currentLevel;
        GameManager.instance.AddMoney(moneyEarned);
    }

    // The math formula for idle game cost scaling
    public double GetUpgradeCost()
    {
        return data.baseCost * System.Math.Pow(data.costMultiplier, currentLevel);
    }

    // Triggered when the player clicks the Upgrade button
    public void BuyUpgrade()
    {
        double cost = GetUpgradeCost();

        if (GameManager.instance.CanAfford(cost))
        {
            GameManager.instance.SpendMoney(cost);
            currentLevel++;
            UpdateUI(); // Refresh the text with the new level and new cost
        }
        else
        {
            Debug.Log("Not enough money to upgrade!");
        }
    }

    private void UpdateUI()
    {
        levelText.text = "Lvl " + currentLevel;
        // Ask the GameManager to format the upgrade cost nicely!
        costText.text = "Upgrade: " + GameManager.instance.FormatMoney(GetUpgradeCost());
    }
}