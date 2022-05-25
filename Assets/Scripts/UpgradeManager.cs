using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] List<Upgrade> upgrades = new List<Upgrade>();


    public void Upgrade(Upgrade upgradeType)
    {
        upgradeType.upgradeLevel += 1;

        //player stat corresponding to the upgradetype goes up (gets multiplied by the multiplier)
        //points are withdrawn from the overal points
        //upgrade price goes up (upgrade level * by price? set multiplier * by price?)
    }
}
