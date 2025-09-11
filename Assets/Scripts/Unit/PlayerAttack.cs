using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bullet;
    public GameObject Player;
    public Transform pos;
    public float cooltime;
    public float Bigcooltime;
    private float curtime;
    private float Bigcurtime;

    private Player Icount;

    void Start()
    {
        Icount = Player.GetComponent<Player>();
    }

    void Update()
    {
        if (curtime <= 0)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                GameObject nearestEnemy = null;
                float shortestDistance = Mathf.Infinity;

                foreach (GameObject enemy in enemies)
                {
                    float dist = Vector2.Distance(transform.position, enemy.transform.position);
                    if (dist < shortestDistance)
                    {
                        shortestDistance = dist;
                        nearestEnemy = enemy;
                    }
                }

                if (bullet != null && nearestEnemy != null)
                {
                    Vector2 dir = (nearestEnemy.transform.position - pos.position).normalized;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                    GameObject firedBullet = Instantiate(bullet, pos.position, Quaternion.Euler(0, 0, angle - 90f));

                    BulletFollow followScript = firedBullet.GetComponent<BulletFollow>();
                    if (followScript != null)
                    {
                        followScript.target = nearestEnemy.transform;
                    }
                }
            }
            curtime = cooltime;
        }
        curtime -= Time.deltaTime;

        if (Bigcurtime < 0)
        {
            if (Icount.ItemCount > 0)
            {
                if (Input.GetKey(KeyCode.X))
                {
                    Debug.Log("필살기 사용");
                    // 모든 적 총알 찾기
                    GameObject[] enemyAttacks = GameObject.FindGameObjectsWithTag("EnemyAttack");

                    // 가까운 순으로 정렬
                    List<GameObject> sortedAttacks = new List<GameObject>(enemyAttacks);
                    sortedAttacks.Sort((a, b) =>
                    {
                        float distA = Vector2.Distance(transform.position, a.transform.position);
                        float distB = Vector2.Distance(transform.position, b.transform.position);
                        return distA.CompareTo(distB);
                    });

                    // 코루틴 실행 → 순서대로 제거
                    StartCoroutine(DestroyBulletsGradually(sortedAttacks));
                    Icount.ItemCount -= 1;
                }
                Bigcurtime = Bigcooltime;
            }
        }
        Bigcurtime -= Time.deltaTime;
    }

    private IEnumerator DestroyBulletsGradually(List<GameObject> bullets)
    {
        float delay = 0.001f;

        foreach (GameObject bullet in bullets)
        {
            if (bullet != null)
            {
                Destroy(bullet);
            }
            yield return new WaitForSeconds(delay); // 잠깐 기다렸다가 다음 총알 제거
        }
    }
}
