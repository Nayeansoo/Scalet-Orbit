using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigGunBullet : MonoBehaviour
{
    public float speed;
    void Start()
    {
        Invoke("DestoryBullet", 1f);

    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("ИэСп");
            DestoryBullet();
        }
    }

    void DestoryBullet()
    {
        Destroy(gameObject);
    }
}
