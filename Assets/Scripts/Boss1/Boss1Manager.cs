using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Manager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject bulletPrefab1;
    public GameObject bulletPrefab2;
    public GameObject bulletPrefab3;

    private bool hasStarted = false;
    private Boss1 boss1;

    void Start()
    {
        boss1 = GetComponent<Boss1>();
    }

    void Update()
    {
        if (gameObject.activeSelf && !hasStarted && boss1.Boss1HP == 10000f)
        {
            StartCoroutine(EnemyPatternUpdate());
        }
    }

    IEnumerator EnemyPatternUpdate()
    {
        hasStarted = true;
        StartCoroutine(Boss_1Pattern0());
        yield return new WaitForSeconds(7.5f);
        StartCoroutine(Boss_1Pattern1());
        yield return new WaitForSeconds(4f);
        StartCoroutine(Boss_1Pattern2());
    }

    IEnumerator Boss_1Pattern0()
    {
        //아래로 이동
        Vector3 targetPos = new Vector3(-3.53f, 2.5f, 0);
        float moveSpeed = 1.2f;
        while (Vector3.Distance(transform.position, targetPos) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(Pattern0());
    }
    IEnumerator Pattern0()
    {
        int count = 400; // 탄 수 증가
        for (int i = 0; i < count; i++)
        {
            float angle = Random.Range(360f, 180f);
            float rad = angle * Mathf.Deg2Rad;
            float speed = Random.Range(4f, 7f);

            Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);

            GameObject bullet = Instantiate(bulletPrefab3, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.velocity = dir * speed;

            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator Boss_1Pattern1()
    {
        // 이동
        Vector3 targetPos = new Vector3(-6f, 0.5f, 0);
        float moveSpeed = 8f;

        while (Vector3.Distance(transform.position, targetPos) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(0.5f);

        // 탄환 순차 발사
        List<GameObject> storedBullets = new List<GameObject>();
        int bulletCount = 5;
        float spacing = 1f;

        for (int i = 0; i < bulletCount; i++)
        {
            float yOffset = (i - bulletCount / 2f) * spacing;
            Vector3 spawnPos = transform.position + new Vector3(1.2f, yOffset, 0); // ← X 방향으로 앞으로 뺌 (보스 기준 오른쪽)

            GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
            storedBullets.Add(bullet);

            yield return new WaitForSeconds(0.2f);
        }


        yield return new WaitForSeconds(0.6f); // 터지기 전 연출용 대기

        foreach (GameObject b in storedBullets)
        {
            if (b != null)
            {
                StartCoroutine(Pattern1(b));
                Destroy(b);
            }

            yield return new WaitForSeconds(0.5f);
        }

        yield return null;
    }
    IEnumerator Pattern1(GameObject bullet)
    {
        int count = 30;
        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            float angle = angleStep * i;
            float rad = Mathf.Deg2Rad * angle;
            Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);

            GameObject newBullet = Instantiate(bulletPrefab2, bullet.transform.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().velocity = dir * 3f;
        }

        yield return null;
    }

    IEnumerator Boss_1Pattern2()
    {
        // 이동
        Vector3 targetPos = new Vector3(-0.73f, 0.5f, 0);
        float moveSpeed = 8f;

        while (Vector3.Distance(transform.position, targetPos) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(0.5f);

        // 탄환 순차 발사
        List<GameObject> storedBullets = new List<GameObject>();
        int bulletCount = 5;
        float spacing = 1f;

        for (int i = 0; i < bulletCount; i++)
        {
            float yOffset = (i - bulletCount / 2f) * spacing;
            Vector3 spawnPos = transform.position + new Vector3(-1.2f, yOffset, 0); // ← X 방향으로 앞으로 뺌 (보스 기준 오른쪽)

            GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
            storedBullets.Add(bullet);

            yield return new WaitForSeconds(0.2f);
        }


        yield return new WaitForSeconds(0.6f); // 터지기 전 연출용 대기

        foreach (GameObject b in storedBullets)
        {
            if (b != null)
            {
                StartCoroutine(Pattern2(b));
                Destroy(b);
            }

            yield return new WaitForSeconds(0.5f);
        }

        yield return null;
    }
    IEnumerator Pattern2(GameObject bullet)
    {
        int count = 30;
        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            float angle = angleStep * i;
            float rad = Mathf.Deg2Rad * angle;
            Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);

            GameObject newBullet = Instantiate(bulletPrefab2, bullet.transform.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().velocity = dir * 3f;
        }

        yield return null;
    }

}
