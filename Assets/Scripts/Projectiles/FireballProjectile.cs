using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : Projectile
{
    public override float damage
    { get => PlayerController.instance.playerStats.damage; set => PlayerController.instance.playerStats.damage = value; }
}
