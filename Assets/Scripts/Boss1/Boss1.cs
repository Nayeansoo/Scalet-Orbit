using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public float Boss1HP;

    void Awake()
    {
        gameObject.SetActive(true);
    }

    void Start()
    {
        Boss1HP = 10000;
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
            Boss1HP -= 2f;
        }

        if (other.CompareTag("subBullet"))
        {
            Boss1HP -= 0.5f;
        }
    }

    void DestroyBoss1()
    {
        Debug.Log("보스 처치");
        Destroy(gameObject);
    }
}
