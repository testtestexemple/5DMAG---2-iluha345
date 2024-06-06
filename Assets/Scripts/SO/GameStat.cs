using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStat : MonoBehaviour
{
    //скріпт для кінцевої статистики
    public int waves;
    public int allCoins;
    public int killedZombie;
    public float allTime;
    public int allUpdates;
    public int allDistance;
    public int allHits;
    public int allNoMissedHits;
    public int allCritycalHits;

    public TMP_Text text;

    public PlayerStat pStat;

    private void Start()
    {
        pStat.stat = gameObject.GetComponent<GameStat>();
        StartCoroutine("Seconds");
    }
    public void SetDef()
    {
      waves = 0;
      allCoins = 0;
      killedZombie = 0;
      allTime = 0;
      allUpdates = 0;
      allDistance = 0;
        allHits = 0;
        allNoMissedHits = 0;
        allCritycalHits = 0;
}
    public void SetStat()
    {
        text.text = $"Ти прожив {waves} днів\r\n" +
            $"Ти здобув {allCoins} монет\r\n" +
            $"Ти вбив {killedZombie} зомбя\r\n" +
            $"Ти провів {allTime/60} хвилин в грі\r\n" +
            $"Ти зробив {allUpdates} покращень\r\n" +
            $"Ти пробіг {allDistance} метри\r\n" +
            $"Ти вдарив мечем {allHits} разів\r\n" +
            $"З них попав {allNoMissedHits} рази\r\n" +
            $"З них крітом {allCritycalHits} рази";
    }
    IEnumerator Seconds()
    {
        yield return new WaitForSeconds(1);
        allTime++;
        StartCoroutine("Seconds");
    }
}
