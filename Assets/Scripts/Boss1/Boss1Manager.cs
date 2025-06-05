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
            hasStarted = true;
            StartCoroutine(Boss_1Pattern());
        }
    }

    IEnumerator Boss_1Pattern()
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
        int count = 30;
        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            float angle = angleStep * i;
            float rad = Mathf.Deg2Rad * angle;
            Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);

            GameObject bullet = Instantiate(bulletPrefab3, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = dir * 3f;
        }

        yield return null;
    }
}
