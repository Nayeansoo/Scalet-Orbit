using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public float Boss1HP;
    void Start()
    {
        Boss1HP = 10000f;
    }

    void Update()
    {
        if (Boss1HP <= 0)
        {
            DestroyBoss1();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Boss1HP -= 1f;
        }

        if (other.CompareTag("BigBullet"))
        {
            Boss1HP -= 500f;
        }

        if (other.CompareTag("subBullet"))
        {
            Boss1HP -= 0.05f;
        }
    }

    void DestroyBoss1()
    {
        Debug.Log("보스 처치");
        Destroy(gameObject);
    }
}
