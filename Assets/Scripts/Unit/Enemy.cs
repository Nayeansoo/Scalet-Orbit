using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float EnemyHP;
    [SerializeField] private GameObject itemPrefab;
    void Start()
    {

    }

    void Update()
    {
        if(EnemyHP <= 0)
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

        if (other.CompareTag("BigBullet"))
        {
            DestroyEnemy();
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
        Debug.Log("Àû Ã³Ä¡");
        Destroy(gameObject);
    }
}
