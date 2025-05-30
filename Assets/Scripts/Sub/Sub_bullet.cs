using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sub_bullet : MonoBehaviour
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
            Debug.Log("명중");
            DestoryBullet();
        }
    }

    void DestoryBullet()
    {
        Destroy(gameObject);
    }
}
