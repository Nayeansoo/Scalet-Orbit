using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public float EnemyHP;
    [SerializeField] private GameObject itemPrefab;
    public GameObject laser;
    public GameObject effect;
    public GameObject effectSignal;

    void Start()
    {
        effectSignal.SetActive(false);
        effect.SetActive(false);
        StartCoroutine(LaserEffect());
    }

    IEnumerator LaserEffect()
    {
        yield return new WaitForSeconds(2f);
        effect.SetActive(true);
        StartCoroutine(Cooldown());
        yield return new WaitForSeconds(0.5f);
        effectSignal.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine (Laser());
        yield return new WaitForSeconds(3.5f);
    }

    IEnumerator Laser()
    {
        effectSignal.SetActive(false);
        yield return new WaitForSeconds(1f);
        laser.SetActive(true);
    }

    IEnumerator Cooldown()
    {
        SpriteRenderer sr = effect.GetComponent<SpriteRenderer>();

        if (sr == null) yield break;

        float blinkInterval = 0.1f;
        float timer = 0f;

        while (timer < 0.3f)
        {
            sr.color = new Color(1f, 1f, 1f, 0.3f);
            yield return new WaitForSeconds(blinkInterval);

            sr.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(blinkInterval);

            timer += blinkInterval * 2f;
        }
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
