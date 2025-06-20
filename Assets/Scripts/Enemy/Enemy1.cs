using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public float EnemyHP;
    [SerializeField] private GameObject itemPrefab;
    public GameObject laser;
    public GameObject effect;

    void Start()
    {
        effect.SetActive(false);
        StartCoroutine(Laser());
    }

    IEnumerator Laser()
    {
        yield return new WaitForSeconds(2f);
        effect.SetActive(true);
    }

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
