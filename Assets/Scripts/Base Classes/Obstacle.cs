using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : Damager
{
    [SerializeField] protected float _damage;
    public override float damage { get => _damage; set => _damage = value; }
}
