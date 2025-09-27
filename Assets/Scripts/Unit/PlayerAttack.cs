using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bullet;     // 발사할 총알 프리팹
    public GameObject Player;     // 플레이어 오브젝트
    public Transform pos;         // 총알 발사 위치
    public float cooltime;        // 일반 공격 쿨타임
    public float Bigcooltime;     // 필살기 쿨타임
    private float curtime;
    private float Bigcurtime;

    private Player Icount;

    void Start()
    {
        Icount = Player.GetComponent<Player>();
    }

    void Update()
    {
        // 일반 공격
        if (curtime <= 0)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                if (bullet != null)
                {
                    // 총알 생성
                    GameObject firedBullet = Instantiate(bullet, pos.position, pos.rotation);

                    // Rigidbody2D에 속도 부여해서 직선 이동하도록 함
                    Rigidbody2D rb = firedBullet.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.velocity = pos.up * 10f; // pos의 위쪽 방향으로 발사 (10은 속도)
                    }
                }
                curtime = cooltime;
            }
        }
        curtime -= Time.deltaTime;

        // 필살기
        if (Bigcurtime < 0)
        {
            if (Icount.ItemCount > 0)
            {
                if (Input.GetKey(KeyCode.X))
                {
                    Debug.Log("필살기 사용");

                    GameObject[] enemyAttacks = GameObject.FindGameObjectsWithTag("EnemyAttack");

                    List<GameObject> sortedAttacks = new List<GameObject>(enemyAttacks);
                    sortedAttacks.Sort((a, b) =>
                    {
                        float distA = Vector2.Distance(transform.position, a.transform.position);
                        float distB = Vector2.Distance(transform.position, b.transform.position);
                        return distA.CompareTo(distB);
                    });

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
            yield return new WaitForSeconds(delay);
        }
    }
}
