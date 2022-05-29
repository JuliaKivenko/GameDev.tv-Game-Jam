using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeDisplay : MonoBehaviour
{
    public Upgrade upgrade;

    [SerializeField] TextMeshProUGUI upgradeName;
    [SerializeField] Image upgradeImage;
    [SerializeField] TextMeshProUGUI upgradeLevel;
    [SerializeField] TextMeshProUGUI upgradePrice;
    [SerializeField] AudioSource upgradeSFX;



    private void Start()
    {
        upgradeName.text = upgrade.upgradeName;
        upgradeImage.sprite = upgrade.upgradeVisual;
        upgradeLevel.text = "Lvl. " + upgrade.upgradeLevel.ToString();
        upgradePrice.text = upgrade.upgradePrice.ToString() + "$ Buy";
    }

    public void BuyUpgradeAndUpdateVisual()
    {
        if (GameManager.sharedInstance.points < upgrade.upgradePrice)
        {
            //Show Message that cannot buy an upgrade. Alternatively just grey out the button
            return;
        }
        upgrade.BuyUpgrade();
        upgradeSFX.Play();

        upgradeLevel.text = "Lvl. " + upgrade.upgradeLevel.ToString();
        upgradePrice.text = upgrade.upgradePrice.ToString() + "$ Buy";

    }
}
