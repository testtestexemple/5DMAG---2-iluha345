using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDStatistic : MonoBehaviour
{
    public PlayerStat stat;

    public TMP_Text speed;
    public TMP_Text additionalDamage;
    //public TMP_Text health;
    public TMP_Text moneyMultipler;
    private void Update()
    {
        speed.text = stat.Speed.ToString();
        additionalDamage.text = stat.AdditionalDamage.ToString();
        //health.text = $"+{stat.Health}"; //hp regen removed for difficulty
        moneyMultipler.text = $"x{stat.MoneyMultipler}";
    }
}