using System.Collections;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bulletPrefab;
    public GameObject bulletPrefab1;
    public GameObject Boss1;
    private GameObject boss1Instance;

    private GameObject SpawnEnemy(Vector3 position)
    {
        return Instantiate(enemyPrefab, position, Quaternion.identity);
    }

    void Start()
    {
        StartCoroutine(EnemyPatternUpdate());
    }

    IEnumerator EnemyPatternUpdate()
    {
        //yield return new WaitForSeconds(2f);
        //yield return StartCoroutine(Pattern0());
        //yield return new WaitForSeconds(3.5f);
        //yield return StartCoroutine(Pattern1());
        //yield return new WaitForSeconds(5f);
        //yield return StartCoroutine(Pattern2());
        //yield return StartCoroutine(Pattern3());
        //yield return new WaitForSeconds(5.5f);
        //yield return StartCoroutine(Pattern4());
        //yield return new WaitForSeconds(1f);
        //yield return StartCoroutine(Pattern5());
        //yield return StartCoroutine(Pattern6());
        //yield return new WaitForSeconds(2f);
        //yield return StartCoroutine(Pattern7());
        //yield return new WaitForSeconds(2.5f);
        yield return StartCoroutine(Patternover());
        boss1Instance = Instantiate(Boss1, new Vector3(-3.53f, 7f, 0), Quaternion.identity);
    }  //적의 패턴을 호출하는 곳 패턴을 추가 하면 여기 밑에 넣어야 나옴

    IEnumerator HomingAfterDelay(GameObject bullet, float delay, float speed)
    {
        yield return new WaitForSeconds(delay);

        GameObject player = GameObject.FindWithTag("Player");
        if (bullet != null && player != null)
        {
            Vector2 dir = (player.transform.position - bullet.transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = dir * speed;
        }
    }

    IEnumerator Pattern0()
    {
        GameObject enemy = SpawnEnemy(new Vector3(-3, 6, 0));

        StartCoroutine(MoveEnemy0(enemy, Vector3.down, 2f, 2f));
        StartCoroutine(MoveEnemy0(enemy, Vector3.up, 2f, 2f, 2f));
        StartCoroutine(BulletPattern0(enemy, 0.5f));

        yield return new WaitForSeconds(1.5f);

        GameObject enemy1 = SpawnEnemy(new Vector3(-6, 6, 0));
        GameObject enemy2 = SpawnEnemy(new Vector3(0, 6, 0));

        StartCoroutine(MoveEnemy0(enemy1, Vector3.down, 2f, 2));
        StartCoroutine(MoveEnemy0(enemy1, Vector3.up, 2f, 2f, 2f));
        StartCoroutine(BulletPattern0(enemy1, 0.5f));

        StartCoroutine(MoveEnemy0(enemy2, Vector3.down, 2f, 2f));
        StartCoroutine(MoveEnemy0(enemy2, Vector3.up, 2f, 2f, 2f));
        StartCoroutine(BulletPattern0(enemy2, 0.5f));
    } //적이 가운데, 왼쪽, 오른쪽 순으로 원형 탄 발사하면서 내려옴
    IEnumerator MoveEnemy0(GameObject enemy, Vector3 direction, float duration, float speed, float delay = 0f)
    {
        if (delay > 0f)
            yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            if (enemy == null) yield break;

            enemy.transform.position += direction * speed * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (direction == Vector3.up && enemy != null)
            Destroy(enemy);
    }
    IEnumerator BulletPattern0(GameObject enemy, float fireInterval)
    {
        float baseAngle = 0f;

        while (enemy != null)
        {
            int count = 36;
            float angleStep = count * 15f / count;

            for (int i = 0; i < count; i++)
            {
                float angle = baseAngle + angleStep * i;
                float rad = Mathf.Deg2Rad * angle;
                Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
                Vector3 firePosition = enemy.transform.position + new Vector3(0, -0.2f, 0);
                GameObject bullet = Instantiate(bulletPrefab, firePosition, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = dir * 5f;
            }

            baseAngle += 200f;
            yield return new WaitForSeconds(fireInterval);
        }
    }

    IEnumerator Pattern1()
    {
        GameObject enemy = SpawnEnemy(new Vector3(0, 6, 0));

        StartCoroutine(MoveEnemy0(enemy, Vector3.down, 3f, 2f));
        StartCoroutine(MoveEnemy0(enemy, Vector3.up, 3f, 2f, 2f));
        StartCoroutine(BulletPattern1(enemy, 0.3f));

        yield return null;
    } //적이 왼쪽에서 360도 방향으로 여러줄로 
    IEnumerator BulletPattern1(GameObject enemy, float fireInterval)
    {
        float baseAngle = 0f;
        float radius = 0.2f;

        while (enemy != null)
        {
            int count = 22;
            float angleStep = 360f / count;

            for (int i = 0; i < count; i++)
            {
                float angle = baseAngle + angleStep * i;
                float rad = Mathf.Deg2Rad * angle;
                Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);

                Vector3 firePos = enemy.transform.position + dir * radius;
                GameObject bullet = Instantiate(bulletPrefab1, firePos, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = dir * 3f;
            }

            baseAngle += 17f;
            radius += 0.1f;
            yield return new WaitForSeconds(fireInterval);
        }
    }

    IEnumerator Pattern2()
    {
        GameObject enemy = SpawnEnemy(new Vector3(-3, 6, 0));
        StartCoroutine(MoveEnemy0(enemy, Vector3.down, 1.5f, 2f));
        StartCoroutine(MoveEnemy0(enemy, Vector3.up, 1.5f, 2f, 3f));
        yield return StartCoroutine(BulletPattern2(enemy, 0.1f, 3f));
        
        yield return new WaitForSeconds(3f);

        if (enemy != null)
            Destroy(enemy);
    } //적이 가운데에서 내려오면서 물결모양 패턴
    IEnumerator BulletPattern2(GameObject enemy, float fireInterval, float duration)
    {
        float time = 0f;
        float elapsed = 0f;

        while (enemy != null && elapsed < duration)
        {
            int count = 4;
            float spacing = 1f;

            for (int i = 0; i < count; i++)
            {
                Vector3 offset = new Vector3((i - count / 2) * spacing, 0, 0);
                Vector3 firePos = enemy.transform.position + offset;

                GameObject bullet = Instantiate(bulletPrefab, firePos, Quaternion.identity);

                float wave = Mathf.Sin(time + i * 0.5f);
                Vector2 direction = new Vector2(wave, -1).normalized;

                bullet.GetComponent<Rigidbody2D>().velocity = direction * 4f;
            }

            time += 0.3f;
            elapsed += fireInterval;

            yield return new WaitForSeconds(fireInterval);
        }
    }

    IEnumerator Pattern3()
    {
        GameObject enemy = SpawnEnemy(new Vector3(-6, 6, 0));

        StartCoroutine(MoveEnemy0(enemy, Vector3.down, 3f, 2f));
        StartCoroutine(MoveEnemy0(enemy, Vector3.up, 3f, 2f, 2f));
        StartCoroutine(BulletPattern3(enemy, 0.3f));

        yield return null;
    } //적이 오른쪽에서 360도 방향으로 여러줄로 1번 배턴하고 같고 위치만 다른 패턴
    IEnumerator BulletPattern3(GameObject enemy, float fireInterval)
    {
        float baseAngle = 0f;
        float radius = 0.2f;

        while (enemy != null)
        {
            int count = 22;
            float angleStep = 360f/ count;

            for (int i = 0; i < count; i++)
            {
                float angle = baseAngle + angleStep * i;
                float rad = Mathf.Deg2Rad * angle;
                Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);

                Vector3 firePos = enemy.transform.position + dir * radius;
                GameObject bullet = Instantiate(bulletPrefab1, firePos, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = dir * 3f;
            }

            baseAngle -= 17f;
            radius += 0.1f;
            yield return new WaitForSeconds(fireInterval);
        }
    }

    IEnumerator Pattern4()
    {
        Vector3[] positions = {
        new Vector3(0, 6, 0),
        new Vector3(0, 8, 0),
        new Vector3(0, 10, 0)
    };

        GameObject[] enemies = new GameObject[positions.Length];

        for (int i = 0; i < positions.Length; i++)
        {
            enemies[i] = SpawnEnemy(positions[i]);

            
            StartCoroutine(MoveEnemy0(enemies[i], Vector3.down, 3f, 2f));
            StartCoroutine(MoveEnemy0(enemies[i], Vector3.up, 3f, 2f, 3f));

            
            StartCoroutine(BulletPattern4(enemies[i], 0.3f));
        }

        yield return new WaitForSeconds(7f);
    } //적이 오른쪽에서 부터 3마리가 순서대로 내려오며 플레이어를 따라가는 총알 발사 
    IEnumerator BulletPattern4(GameObject enemy, float fireInterval)
    {
        while (enemy != null)
        {
            int count = 1;
            float spacing = 1f;

            for (int i = 0; i < count; i++)
            {
                Vector3 offset = new Vector3((i - count / 2) * spacing, -0.2f, 0);
                GameObject bullet = Instantiate(bulletPrefab, enemy.transform.position + offset, Quaternion.identity);

                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = Vector2.down * 3f;

                StartCoroutine(HomingAfterDelay(bullet, 1f, 4f));
            }

            yield return new WaitForSeconds(fireInterval);
        }
    }

    IEnumerator Pattern5()
    {
        Vector3[] positions = {
        new Vector3(-6, 6, 0),
        new Vector3(-6, 8, 0),
        new Vector3(-6, 10, 0)
    };

        GameObject[] enemies = new GameObject[positions.Length];

        for (int i = 0; i < positions.Length; i++)
        {
            enemies[i] = SpawnEnemy(positions[i]);


            StartCoroutine(MoveEnemy0(enemies[i], Vector3.down, 3f, 2f));
            StartCoroutine(MoveEnemy0(enemies[i], Vector3.up, 3f, 2f, 3f));


            StartCoroutine(BulletPattern4(enemies[i], 0.3f));
        }

        yield return new WaitForSeconds(7f);
    } //4번패턴과 같지만 왼쪽에서 내려옴
    IEnumerator BulletPattern5(GameObject enemy, float fireInterval)
    {
        while (enemy != null)
        {
            int count = 1;
            float spacing = 1f;

            for (int i = 0; i < count; i++)
            {
                Vector3 offset = new Vector3((i - count / 2) * spacing, -0.2f, 0);
                GameObject bullet = Instantiate(bulletPrefab1, enemy.transform.position + offset, Quaternion.identity);

                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = Vector2.down * 3f;

                StartCoroutine(HomingAfterDelay(bullet, 1f, 4f));
            }

            yield return new WaitForSeconds(fireInterval);
        }
    }

    IEnumerator Pattern6()
    {
        GameObject enemy = SpawnEnemy(new Vector3(-3, 6, 0));

        StartCoroutine(MoveEnemy0(enemy, Vector3.down, 3f, 2f));
        StartCoroutine(MoveEnemy0(enemy, Vector3.up, 3f, 2f, 3f));
        StartCoroutine(BulletPattern6(enemy, 1f));

        yield return new WaitForSeconds(6f);
    }  //적에서 원형으로 총알이 많이 나옴 진짜 많이 나옴
    IEnumerator BulletPattern6(GameObject enemy, float fireInterval)
    {
        while (enemy != null)
        {
            int count = 17;
            float angleStep = 360f / count;

            for (int i = 0; i < count; i++)
            {
                float angle = angleStep * i;
                float rad = Mathf.Deg2Rad * angle;
                Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

                GameObject bullet = Instantiate(bulletPrefab, enemy.transform.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = dir * 1.5f;

                StartCoroutine(ExplodeBullet6(bullet, 1.5f, 5f));
            }

            yield return new WaitForSeconds(fireInterval);
        }
    }
    IEnumerator ExplodeBullet6(GameObject bullet, float delay, float speed)
    {
        yield return new WaitForSeconds(delay);

        if (bullet == null) yield break;

        Vector2 pos = bullet.transform.position;
        Destroy(bullet); // 기존 탄 삭제

        // 중앙에서 새로운 폭발탄 생성
        int count = 12;
        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            float angle = angleStep * i;
            float rad = Mathf.Deg2Rad * angle;
            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().velocity = dir * speed;
        }
    }

    IEnumerator Pattern7()
    {
        int enemyCount = 5;
        float interval = 0.5f;

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPos = new Vector3(-10f, 4f + i * 0.65f, 0);
            GameObject enemy = SpawnEnemy(spawnPos);

            StartCoroutine(CurveInOutMove7(enemy, 3f));
            yield return new WaitForSeconds(interval);
        }

        yield return new WaitForSeconds(6f);
    }   //적이 왼쪽에서 나오며 원형으로 뿌리고 이동하다가 정해진 위치에서 원형으로 다시 탄 날리기
    IEnumerator CurveInOutMove7(GameObject enemy, float duration)
    {
        float elapsed = 0f;
        float frequency = 2f;
        float amplitude = 1.5f;

        float fireTimer = 0f;
        float fireInterval = 0.5f;

        while (elapsed < duration)
        {
            if (enemy == null) yield break;

            float x = Mathf.Lerp(-10f, 0f, elapsed / duration);
            float y = Mathf.Sin(x * frequency) * amplitude + 4f;
            enemy.transform.position = new Vector3(x, y, 0);

            fireTimer += Time.deltaTime;
            if (fireTimer >= fireInterval)
            {
                FireBulletCircle(enemy.transform.position);
                fireTimer = 0f;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return StartCoroutine(BulletPattern_Circle7(enemy, 0.3f));
        yield return new WaitForSeconds(2f);

        elapsed = 0f;
        while (elapsed < duration)
        {
            if (enemy == null) yield break;

            float x = Mathf.Lerp(0f, 10f, elapsed / duration);
            float y = Mathf.Sin(x * frequency) * amplitude + 4f;
            enemy.transform.position = new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(enemy);
    }
    void FireBulletCircle(Vector3 position)
    {
        int count = 8;
        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            float angle = angleStep * i;
            float rad = Mathf.Deg2Rad * angle;
            Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);

            GameObject bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = dir * 3f;
        }
    }
    IEnumerator BulletPattern_Circle7(GameObject enemy, float fireInterval)
    {
        if (enemy == null) yield break;

        int count = 15;
        float baseAngle = 0f;

        for (int i = 0; i < 3; i++)
        {
            if (enemy == null) yield break;

            for (int j = 0; j < count; j++)
            {
                if (enemy == null) yield break;

                float angle = baseAngle + (360f / count) * j;
                float rad = Mathf.Deg2Rad * angle;
                Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);

                GameObject bullet = Instantiate(bulletPrefab, enemy.transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = dir * 4f;
            }

            baseAngle += 15f;
            yield return new WaitForSeconds(fireInterval);
        }
    }

    IEnumerator Patternover() //모든 패턴이 끝난뒤 카메라 흔듬 모션과 함께 보스 켜주기
    {
        SomeEventOrFunction();
        yield return null;
    }
    void SomeEventOrFunction()
    {
        CameraMove cam = Camera.main.GetComponent<CameraMove>();
        if (cam != null)
        {
            cam.Play();
        }
    }

}