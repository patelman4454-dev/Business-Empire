using UnityEngine;
using TMPro; // We need this to talk to the UI Text

public class GameManager : MonoBehaviour
{
    // This creates the Singleton, allowing other scripts to access it easily
    public static GameManager instance;

    [Header("Economy")]
    public double totalMoney = 0;

    [Header("UI Elements")]
    public TextMeshProUGUI moneyText;

    void Awake()
    {
        // Singleton setup: Make sure there is only ever one GameManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateMoneyUI(); // Update the text right when the game starts
    }

    // Other scripts will call this method to add cash
    public void AddMoney(double amount)
    {
        totalMoney += amount;
        UpdateMoneyUI();
    }

    private void UpdateMoneyUI()
    {
        // "F2" formats the massive double numbers to show exactly two decimal places
        moneyText.text = "$" + totalMoney.ToString("F2");
    }

    // Checks if the player has enough cash
    public bool CanAfford(double amount)
    {
        return totalMoney >= amount;
    }

    // Deducts the cash and updates the screen
    public void SpendMoney(double amount)
    {
        if (CanAfford(amount))
        {
            totalMoney -= amount;
            UpdateMoneyUI();
        }
    }
}