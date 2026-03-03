using UnityEngine;

[CreateAssetMenu(fileName = "New Business", menuName = "Business Empire/Business Data")]
public class BusinessData : ScriptableObject
{
    [Header("Visuals")]
    public string businessName;
    public Sprite businessIcon;

    [Header("Core Economy")]
    public double baseCost;         // The price of level 1
    public double baseRevenue;      // The money it makes at level 1
    public float productionTime;    // How long it takes to pay out (in seconds)

    [Header("Growth Multipliers")]
    // 1.15 is the standard idle game multiplier (costs 15% more each level)
    public float costMultiplier = 1.15f;
}