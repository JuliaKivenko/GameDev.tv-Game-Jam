using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum statTypes
{
    Health,
    Damage
}


[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class Upgrade : ScriptableObject
{
    public string upgradeName;
    public statTypes upgradeType;
    public Sprite upgradeVisual;
    public int upgradePrice;
    public float upgradePriceMultiplier;
    public int upgradeLevel;
    public float upgradeStatIncrease;

    public delegate void BuyUpgradeAction();
    public static event BuyUpgradeAction onBuyUpgrade;

    public void BuyUpgrade()
    {
        //Subtract price from the overal points player has
        GameManager.instance.SubstractPoints(upgradePrice);

        //Invoke OnBuyUpgrade event
        if (onBuyUpgrade != null)
            onBuyUpgrade.Invoke();

        //Multiply the corresponding stat by the upgrade multiplier. Upgrade type which correlates with specific stats? Players stats script which has all the components hooked up to specific stat type? 
        PlayerController.instance.playerStats.ModifyPlayerStat(upgradeType, upgradeStatIncrease);

        //Increase Upgrade Level
        upgradeLevel += 1;

        //Increase price of the next upgrade by multiplier
        upgradePrice = (int)Mathf.Round(upgradePrice * upgradePriceMultiplier);
    }

}
