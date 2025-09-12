using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public float Boss1HP = 10000f; // 보스 초기 HP

    private bool isDead = false; // ✅ 중복 처치 방지

    void Awake()
    {
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (!isDead && Boss1HP <= 0)
        {
            DestroyBoss1();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        if (other.CompareTag("Bullet"))
        {
            Boss1HP -= 2f;
        }

        if (other.CompareTag("subBullet"))
        {
            Boss1HP -= 0.5f;
        }
    }

    void DestroyBoss1()
    {
        isDead = true; // ✅ 여러 번 호출되는 것 방지
        Debug.Log("보스 처치");

        // 🔔 GameClear 실행
        if (GameManager.instance != null)
        {
            GameManager.instance.GameClear();
        }

        Destroy(gameObject);
    }
}
