using UnityEngine;

public class BulletFollow : MonoBehaviour
{
    public Transform target;
    public float speed;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (target != null)
        {
            Vector2 dir = (target.position - transform.position).normalized;
            rb.velocity = dir * speed;

            // 회전도 맞춰주기
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f); // 방향 보정
        }
    }
}
