using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : Projectile
{
    public override float damage
    { get => PlayerController.sharedInstance.playerStats.damage; set => PlayerController.sharedInstance.playerStats.damage = value; }
}
