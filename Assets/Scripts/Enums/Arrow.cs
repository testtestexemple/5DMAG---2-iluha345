using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Arrow : MonoBehaviour
{
    private int speed;
    private int damage;
    private void Start()
    {
        damage = Random.Range(20, 40);
        speed = Random.Range(2, 7);
        Invoke(nameof(Destro), 10);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().ClaimDamage(damage);
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        transform.position += new Vector3(transform.position.x * 0.05f * Time.fixedDeltaTime,0,0);
    }
    private void Destro()
    {
        Destroy(gameObject);
    }
}
