using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stat", menuName = "Stat")]
public class PlayerStat: ScriptableObject
{
    [HideInInspector] public float Speed;
    [HideInInspector] public int AdditionalDamage;
    [HideInInspector] public int Health;
    [HideInInspector] public int MaxHealth;
    [HideInInspector] public int Money;
    [HideInInspector] public float MoneyMultipler;
    [HideInInspector] public float AttackDelay;

    [field: Header("Default Value")]
    [field: SerializeField] public float DefSpeed { get; private set; }
    [field: SerializeField] public int DefAdditionalDamage { get; private set; }
    [field: SerializeField] public int DefMaxHealth { get; private set; }
    [field: SerializeField] public int DefHealth { get; private set; }
    [field: SerializeField] public int DefMoney { get; private set; }
    [field: SerializeField] public float DefMoneyMultipler { get; private set; }
    [field: SerializeField] public float DefAttackDelay { get; private set; }

    public int HealthLevel { get; private set; } = 1;
    public int DamageLevel { get; private set; } = 1;
    public int SpeedLevel { get; private set; } = 1;
    public int AtkSpeedLevel { get; private set; } = 1;
    public int MoneyLevel { get; private set; } = 1;

    public GameStat stat;

    public void SetDefValue()
    {
        Speed = DefSpeed;
        AdditionalDamage = DefAdditionalDamage;
        Health = DefHealth;
        MaxHealth = DefMaxHealth;
        Money = DefMoney;
        MoneyMultipler = DefMoneyMultipler;
        AttackDelay = DefAttackDelay;
    }

    public void UpgradeHealth()
    {
        MaxHealth += 10;
        HealthLevel++;
        stat.allUpdates++;
    }

    public void UpgradeDamage()
    {
        AdditionalDamage++;
        DamageLevel++;
        stat.allUpdates++;
    }

    public void UpgradeSpeed()
    {
        Speed += 0.5f;
        SpeedLevel++;
        stat.allUpdates++;
    }
    public void UpgradeAttackSpeed()
    {
        AttackDelay -= 0.2f;
        AtkSpeedLevel++;
        stat.allUpdates++;
    }

    public void UpgradeMoneyMultiplier()
    {
        MoneyMultipler += 0.1f;
        MoneyLevel++;
        stat.allUpdates++;
    }
}
