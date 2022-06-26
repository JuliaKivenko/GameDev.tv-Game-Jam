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
    [SerializeField] Animator upgradeImageAnimator;
    [SerializeField] TextMeshProUGUI upgradeLevel;
    [SerializeField] TextMeshProUGUI upgradePrice;
    [SerializeField] AudioClip upgradeSFX;
    [SerializeField] AudioClip unavailableSFX;



    private void Start()
    {
        upgradeName.text = upgrade.upgradeName;
        upgradeImage.sprite = upgrade.upgradeVisual;
        upgradeLevel.text = "Lvl. " + upgrade.upgradeLevel.ToString();
        upgradePrice.text = upgrade.upgradePrice.ToString() + "$ Buy";
    }

    public void BuyUpgrade()
    {
        if (GameManager.instance.points < upgrade.upgradePrice)
        {
            //Show Message that cannot buy an upgrade. Alternatively just grey out the button
            SoundManager.PlaySound(unavailableSFX);
            return;
        }
        upgrade.BuyUpgrade();
        upgradeImageAnimator.Play("A_UpgradeImageOnUpgrade");
        SoundManager.PlaySound(upgradeSFX);

        UpdateVisual();

    }

    public void UpdateVisual()
    {
        upgradeLevel.text = "Lvl. " + upgrade.upgradeLevel.ToString();
        upgradePrice.text = upgrade.upgradePrice.ToString() + "$ Buy";
    }
}
