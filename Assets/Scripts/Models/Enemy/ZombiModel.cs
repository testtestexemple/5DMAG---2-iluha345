using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombiModel : EnemyModel
{
    public ZombiModel()
    {
        wave = 1;

        StartHealth = 10;
        StartDamage = 5;
        StartReward = Random.Range(3, 6);
        StartSpeed = 5;
        ChaseDistance = 5;

        Health = StartHealth;
        Damage = StartDamage;
        Reward = StartReward;
    }
}
