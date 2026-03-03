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
        // We use our new formatter instead of just ToString!
        moneyText.text = FormatMoney(totalMoney);
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

    // Converts massive numbers into clean UI text (e.g., 1500 -> 1.50K)
    public string FormatMoney(double value)
    {
        // If it's less than 1,000, just show the normal number
        if (value < 1000)
        {
            return "$" + value.ToString("F2");
        }

        // The suffixes for massive numbers
        string[] suffixes = { "", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No", "Dc" };
        int suffixIndex = 0;

        // Keep dividing by 1000 until the number is small enough, and move up the suffix list
        while (value >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            value /= 1000;
            suffixIndex++;
        }

        // Return the shrunken number with the correct letter attached
        return "$" + value.ToString("F2") + suffixes[suffixIndex];
    }
}