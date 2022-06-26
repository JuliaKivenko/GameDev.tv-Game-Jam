using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public override float baseHealth
    { get => PlayerController.instance.playerStats.health; set { } }

}
