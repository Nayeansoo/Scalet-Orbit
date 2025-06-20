using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float EnemyHP;
    [SerializeField] private GameObject itemPrefab;

    void Update()
    {
        if (EnemyHP <= 0)
        {
            DropItem();
            DestroyEnemy();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            EnemyHP -= 1f;
        }

        if (other.CompareTag("subBullet"))
        {
            EnemyHP -= 0.05f;
        }
    }

    void DropItem()
    {
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }
    }

    void DestroyEnemy()
    {
        Debug.Log("적 처치");
        Destroy(gameObject);
    }
}
