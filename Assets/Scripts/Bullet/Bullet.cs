using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    void Start()
    {
        Invoke("DestoryBullet", 1f);
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            DestoryBullet();
        }

        if (other.CompareTag("Enemy2"))
        {
            DestoryBullet();
        }

        if (other.CompareTag("Enemy3")){
            DestoryBullet();
        }
    }

    void DestoryBullet()
    {
        Destroy(gameObject);
    }
}
