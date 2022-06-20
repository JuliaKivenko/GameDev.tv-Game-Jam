using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public override float baseHealth
    { get => PlayerController.sharedInstance.playerStats.health; }
    //make it so that one cannot set 

}
