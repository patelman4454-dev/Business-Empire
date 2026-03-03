using UnityEngine;
using UnityEngine.UI; // We need this for the Image/Progress Bar
using TMPro;

public class ClickManager : MonoBehaviour
{
    [Header("Click Power")]
    public double baseMoneyPerClick = 1.0;
    public double currentMoneyPerClick = 1.0;
    public int currentLevel = 1;

    [Header("Upgrade Costs")]
    public double baseUpgradeCost = 1000.0; // Starts at $1000 like you asked
    public float costMultiplier = 1.5f;     // Cost goes up by 50% each level

    [Header("UI Elements")]
    public Image progressBarFill;
    public Button upgradeButton;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI earnPerClickText;

    void Start()
    {
        UpdateClickUI();
    }

    void Update()
    {
        // 1. Get current money and cost
        double currentMoney = GameManager.instance.totalMoney;
        double currentCost = GetUpgradeCost();

        // 2. Fill the progress bar based on how much money you have
        // We cast to float because UI fillAmount only takes floats from 0.0 to 1.0
        float progress = (float)(currentMoney / currentCost);
        progressBarFill.fillAmount = Mathf.Clamp01(progress); // Clamp prevents it from going over 100%

        // 3. Enable or disable the Upgrade button automatically
        if (currentMoney >= currentCost)
        {
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeButton.interactable = false;
        }
    }

    // Called when tapping the center of the screen
    public void ClickToEarn()
    {
        GameManager.instance.AddMoney(currentMoneyPerClick);
    }

    // Calculates the cost of the next level
    public double GetUpgradeCost()
    {
        // Level 1: 1000 * 1.5^0 = 1000
        // Level 2: 1000 * 1.5^1 = 1500, etc.
        return baseUpgradeCost * System.Math.Pow(costMultiplier, currentLevel - 1);
    }

    // Called when the Upgrade button is clicked
    public void BuyUpgrade()
    {
        double cost = GetUpgradeCost();

        if (GameManager.instance.CanAfford(cost))
        {
            // Pay the money
            GameManager.instance.SpendMoney(cost);

            // Level up
            currentLevel++;

            // Increase click power (Example: $1 -> $2 -> $3)
            currentMoneyPerClick = baseMoneyPerClick * currentLevel;

            UpdateClickUI();
        }
    }

    private void UpdateClickUI()
    {
        levelText.text = "Level " + currentLevel;
        costText.text = "Cost: " + GameManager.instance.FormatMoney(GetUpgradeCost());
        earnPerClickText.text = GameManager.instance.FormatMoney(currentMoneyPerClick) + " per click";
    }
}