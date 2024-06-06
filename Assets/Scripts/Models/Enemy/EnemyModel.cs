using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyModel
{
    public int StartHealth { get; set; }
    public int StartDamage { get; protected set; }
    public int StartReward { get; protected set; }
    public int StartChaseDistance { get; protected set; }
    public int StartSpeed { get; protected set; }
    public int Health { get; set; }
    public int Damage { get;  set; }
    public int Reward { get;  set; }
    public int ChaseDistance { get; protected set; }
    public int Speed { get;  set; }
    public int wave { get; set; }
}
