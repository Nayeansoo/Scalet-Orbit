using System.Collections;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public float EnemyHP;
    public GameObject itemPrefab;

    public GameObject laser;        // 자식 laser 오브젝트 연결
    public GameObject effect;       // 바닥 효과
    public GameObject effectSignal; // 경고 사인

    private Animator laserAnim;

    void Start()
    {
        if (laser != null)
        {
            laser.SetActive(false); // 시작할 때 꺼두기
            laserAnim = laser.GetComponent<Animator>();
        }

        if (effect != null) effect.SetActive(false);
        if (effectSignal != null) effectSignal.SetActive(false);
    }

    public IEnumerator enemy2Attack()
    {
        // 경고 & 바닥 이펙트
        if (effect != null) effect.SetActive(true);
        if (effectSignal != null) effectSignal.SetActive(true);

        yield return new WaitForSeconds(1f);

        // 레이저 발사
        yield return StartCoroutine(Laser());

        // 종료 후 이펙트 끄기
        if (effect != null) effect.SetActive(false);
        if (effectSignal != null) effectSignal.SetActive(false);
    }

    IEnumerator Laser()
    {
        laser.SetActive(true);
        Animator anim = laser.GetComponent<Animator>();

        if (anim != null)
        {
            anim.Play("Enemy2_Attack", -1, 0f);
            Debug.Log("레이저 애니메이션 실행!");

            // 그냥 20초 동안 유지
            yield return new WaitForSeconds(20f);
        }
        else
        {
            Debug.LogWarning("Animator 없음!");
            yield return new WaitForSeconds(100f); // Animator 없어도 강제로 20초
        }

        laser.SetActive(false);
    }




    void Update()
    {
        if (EnemyHP <= 0)
        {
            DropItem();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet")) EnemyHP -= 1f;
        if (other.CompareTag("subBullet")) EnemyHP -= 0.05f;
    }

    void DropItem()
    {
        if (itemPrefab != null)
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
    }
}
