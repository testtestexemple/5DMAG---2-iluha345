using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpot : MonoBehaviour
{
    private int delay;
    private bool isCanShoot = true;
    public GameObject Arrow;

    public Animator shoot;

    private void Start()
    {
        delay = Random.Range(5, 20);
        StartCoroutine("Delay");
    }
    private void Shoot()
    {
        if (!isCanShoot) return;

        isCanShoot = false;
        Instantiate(Arrow, transform.position, Quaternion.Euler(0,180,90));
        StartCoroutine("Delay");
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        isCanShoot = true;
        shoot.SetTrigger("Shoot");
    }
}
