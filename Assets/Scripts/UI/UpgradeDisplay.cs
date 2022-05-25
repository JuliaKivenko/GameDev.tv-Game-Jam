using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeDisplay : MonoBehaviour
{
    public Upgrade upgrade;

    [SerializeField] TextMeshProUGUI upgradeName;
    [SerializeField] Sprite upgradeImage;
    [SerializeField] TextMeshProUGUI upgradeLevel;
    [SerializeField] TextMeshProUGUI upgradePrice;


    private void Start()
    {
        upgradeName.text = upgrade.upgradeName;
        upgradeImage = upgrade.upgradeVisual;
        upgradeLevel.text = "Lvl. " + upgrade.upgradeLevel.ToString();
        upgradePrice.text = upgrade.upgradePrice.ToString() + "$ Buy";
    }

    public void BuyUpgradeAndUpdateVisual()
    {
        upgrade.BuyUpgrade();

        upgradeLevel.text = "Lvl. " + upgrade.upgradeLevel.ToString();
        upgradePrice.text = upgrade.upgradePrice.ToString() + "$ Buy";

    }
}
