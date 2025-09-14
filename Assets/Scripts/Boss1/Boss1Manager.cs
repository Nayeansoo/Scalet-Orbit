using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Manager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject bulletPrefab1;
    public GameObject bulletPrefab2;
    public GameObject bulletPrefab3;
    public GameObject bulletPrefab4;
    public GameObject bulletPrefab5;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;

    public Transform firePoint;

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
            StartCoroutine(EnemyPatternUpdate());
        }
    }

    IEnumerator EnemyPatternUpdate()
    {
        while (boss1.Boss1HP > 0f)
        {
            transform.position = new Vector3(-3.53f, 2.5f, 0);

            yield return StartCoroutine(Boss_1Pattern0());
            yield return new WaitForSeconds(2.5f);

            yield return StartCoroutine(Boss_1Pattern1());
            yield return new WaitForSeconds(3.5f);

            yield return StartCoroutine(Boss_1Pattern2());
            yield return new WaitForSeconds(2.5f);

            yield return StartCoroutine(Boss_1Pattern3());
            yield return new WaitForSeconds(1f);

            yield return StartCoroutine(Boss_1Pattern4());
            yield return new WaitForSeconds(7f);

            yield return StartCoroutine(Boss_1Pattern5());
            yield return new WaitForSeconds(2f); // 한 사이클 끝난 뒤 잠깐 대기
        }

        // 여기 도착하면 보스 HP가 0이 된 것 → 패턴 종료
        Debug.Log("보스 격파! 패턴 종료");
    }  //잡몹 패턴을 적을때 처럼 여기에도 똑같이 패턴을 추가하면 넣어주기만 하면 됨

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
    }  //보스를 기준으로 360도로 무작위 위치에 탄환을 날림
    IEnumerator Pattern0()
    {
        int count = 500; // 탄 수 증가
        for (int i = 0; i < count; i++)
        {
            float angle = Random.Range(360f, 0f);
            float rad = angle * Mathf.Deg2Rad;
            float speed = Random.Range(4f, 7f);

            Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);

            GameObject bullet = Instantiate(bulletPrefab2, transform.position, Quaternion.identity);
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

            GameObject bullet = Instantiate(bulletPrefab1, spawnPos, Quaternion.identity);
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
    }  //보스가 왼쪽으로 이동후 4개에 탄환을 소환후 그 탄환이 터지며 원형으로 날아감 
    IEnumerator Pattern1(GameObject bullet)
    {
        int count = 30;
        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            float angle = angleStep * i;
            float rad = Mathf.Deg2Rad * angle;
            Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);

            GameObject newBullet = Instantiate(bulletPrefab3, bullet.transform.position, Quaternion.identity);
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

            GameObject bullet = Instantiate(bulletPrefab1, spawnPos, Quaternion.identity);
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
    }  //1번 패턴과 같으나 보스가 오른쪽으로 이동해 탄환을 날림
    IEnumerator Pattern2(GameObject bullet)
    {
        int count = 30;
        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            float angle = angleStep * i;
            float rad = Mathf.Deg2Rad * angle;
            Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);

            GameObject newBullet = Instantiate(bulletPrefab3, bullet.transform.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().velocity = dir * 3f;
        }

        yield return null;
    }

    IEnumerator Boss_1Pattern3()
    {
        Vector3 targetPos = new Vector3(-3.53f, 2.5f, 0); // 보스 위치
        float moveSpeed = 2f;

        while (Vector3.Distance(transform.position, targetPos) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(RadialWaveBullets(25, 0.8f, 10f));
        // 탄 수, 간격, 총 지속 시간
    }  //보스가 위로 올라가 원형 탄환 2번 발사 후 바닥으로 돌진하며 수많은 탄환을 날림
    IEnumerator RadialWaveBullets(int bulletCount, float interval, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float baseAngle = 0f;

            for (int i = 0; i < bulletCount; i++)
            {
                float angle = baseAngle + (360f / bulletCount) * i;
                float rad = angle * Mathf.Deg2Rad;
                Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);

                Vector3 startPos = transform.position;
                StartCoroutine(WavyMoveBullet(startPos, dir, 6f)); // 탄환 수명 6초
            }

            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }
    }
    IEnumerator WavyMoveBullet(Vector3 startPos, Vector3 direction, float lifeTime)
    {
        GameObject bullet = Instantiate(bulletPrefab2, startPos, Quaternion.identity);
        float speed = 2f;
        float waveAmplitude = 1f;
        float waveFrequency = 5f;

        float time = 0f;

        while (time < lifeTime && bullet != null)
        {
            Vector3 offset = Vector3.Cross(direction, Vector3.forward) * Mathf.Sin(time * waveFrequency) * waveAmplitude;
            bullet.transform.position = startPos + direction * speed * time + offset;
            time += Time.deltaTime;
            yield return null;
        }

        if (bullet != null)
            Destroy(bullet);
    }

    IEnumerator Boss_1Pattern4()
    {
        Vector3 startPos = new Vector3(-3.53f, 2.5f, 0);      // 시작 위치
        Vector3 upPos = startPos + new Vector3(0, 2f, 0);     // 위로 이동 위치
        Vector3 dropPos = new Vector3(startPos.x, -4f, 0);    // 아래 돌진 위치

        transform.position = startPos;
        yield return new WaitForSeconds(0.1f);

        float elapsedUp = 0f;
        float upDuration = 1.5f;
        while (elapsedUp < upDuration)
        {
            transform.position = Vector3.Lerp(startPos, upPos, elapsedUp / upDuration);
            elapsedUp += Time.deltaTime;
            yield return null;
        }
        transform.position = upPos;
        yield return new WaitForSeconds(0.5f);

        float elapsedDown = 0f;
        float downDuration = 0.4f;
        while (elapsedDown < downDuration)
        {
            transform.position = Vector3.Lerp(upPos, dropPos, elapsedDown / downDuration);
            elapsedDown += Time.deltaTime;
            yield return null;
        }
        transform.position = dropPos;

        FireBurst(transform.position, 100, 7f);
        yield return new WaitForSeconds(0.5f);

        float elapsedBack = 0f;
        float backDuration = 1.5f;
        while (elapsedBack < backDuration)
        {
            transform.position = Vector3.Lerp(dropPos, startPos, elapsedBack / backDuration);
            elapsedBack += Time.deltaTime;
            yield return null;
        }
        transform.position = startPos;
        yield return new WaitForSeconds(0.5f);
    } //보스가 위에서 옆으로 왔다갔다 하는 원형탄환을 이어서 발사함
    void FireBurst(Vector3 center, int count, float baseSpeed)
    {
        float angleStep = 360f / count;
        float baseAngle = Random.Range(0f, 360f); // 회전 시작 각도 랜덤화

        for (int i = 0; i < count; i++)
        {
            float angle = baseAngle + angleStep * i;
            float rad = Mathf.Deg2Rad * angle;

            Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
            float speed = baseSpeed + Random.Range(-1.5f, 1.5f); // 속도 약간씩 다르게

            GameObject bullet = Instantiate(bulletPrefab4, center, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = dir * speed;
            }
        }
    }

    IEnumerator Boss_1Pattern5()
    {
        // 1. enemyPrefab2 두 마리 소환 (왼쪽/오른쪽)
        GameObject leftEnemy = Instantiate(enemyPrefab2, new Vector3(-6f, 5f, 0), Quaternion.identity);
        GameObject rightEnemy = Instantiate(enemyPrefab2, new Vector3(-1f, 5f, 0), Quaternion.identity);

        // 2. 아래로 이동
        Vector3 leftTarget = new Vector3(-6f, 3f, 0);
        Vector3 rightTarget = new Vector3(-1f, 3f, 0);

        float moveSpeed = 2f;
        while (Vector3.Distance(leftEnemy.transform.position, leftTarget) > 0.05f ||
               Vector3.Distance(rightEnemy.transform.position, rightTarget) > 0.05f)
        {
            if (leftEnemy != null)
                leftEnemy.transform.position = Vector3.MoveTowards(leftEnemy.transform.position, leftTarget, moveSpeed * Time.deltaTime);
            if (rightEnemy != null)
                rightEnemy.transform.position = Vector3.MoveTowards(rightEnemy.transform.position, rightTarget, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // 3. Enemy2 레이저 공격 시작
        if (leftEnemy != null)
            StartCoroutine(leftEnemy.GetComponent<Enemy1>().enemy2Attack());
        if (rightEnemy != null)
            StartCoroutine(rightEnemy.GetComponent<Enemy1>().enemy2Attack());

        // 4. 보스 와리가리 탄막 실행 (20초간)
        float duration = 20f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            FireWavyBullet(transform.position);
            yield return new WaitForSeconds(0.2f); // 발사 간격
            elapsed += 0.2f;
        }

        // 5. 위로 퇴장
        Vector3 leftExit = new Vector3(-6f, 6f, 0);
        Vector3 rightExit = new Vector3(-1f, 6f, 0);

        while ((leftEnemy != null && Vector3.Distance(leftEnemy.transform.position, leftExit) > 0.05f) ||
               (rightEnemy != null && Vector3.Distance(rightEnemy.transform.position, rightExit) > 0.05f))
        {
            if (leftEnemy != null)
                leftEnemy.transform.position = Vector3.MoveTowards(leftEnemy.transform.position, leftExit, moveSpeed * Time.deltaTime);
            if (rightEnemy != null)
                rightEnemy.transform.position = Vector3.MoveTowards(rightEnemy.transform.position, rightExit, moveSpeed * Time.deltaTime);
            yield return null;
        }

        if (leftEnemy != null) Destroy(leftEnemy);
        if (rightEnemy != null) Destroy(rightEnemy);
    }

    // =========================
    // 와리가리 탄막 함수
    // =========================
    void FireWavyBullet(Vector3 startPos)
    {
        // 발사 위치 그대로 사용 (중앙 보정 제거)
        GameObject bullet = Instantiate(bulletPrefab2, startPos, Quaternion.identity);
        StartCoroutine(WavyMove(bullet));
    }

    IEnumerator WavyMove(GameObject bullet)
    {
        float speed = 2f;        // 내려가는 속도
        float amplitude = 2f;    // 좌우 흔들림 크기
        float frequency = 5f;    // 흔들림 속도
        float elapsed = 0f;

        // 보스 위치 기준 (시작 x좌표 고정)
        float centerX = bullet.transform.position.x;

        while (bullet != null && bullet.transform.position.y > -6f)
        {
            elapsed += Time.deltaTime;

            // 중심(centerX)에서 좌우로 흔들리게
            float xOffset = Mathf.Sin(elapsed * frequency) * amplitude;

            bullet.transform.position = new Vector3(
                centerX + xOffset,
                bullet.transform.position.y - speed * Time.deltaTime,
                0
            );

            yield return null;
        }

        if (bullet != null) Destroy(bullet);
    }
}
