using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    
    public float fallSpeed = 0.05f;
    void Start()
    {
        Invoke("DestroyItem", 4f);
    }

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyItem();
        }
    }

    void DestroyItem()
    {    
        Destroy(gameObject);
    }
}
