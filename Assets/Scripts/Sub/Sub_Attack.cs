using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sub_Attack : MonoBehaviour
{
    public GameObject bullet;
    public GameObject Player;
    public Transform pos;

    public float cooltime;
    private float curtime;

    void Start()
    {

    }

    void Update()
    {
        if (curtime < 0)
        {
                if (Input.GetKey(KeyCode.Z))
                {
                    Instantiate(bullet, pos.position, transform.rotation);
                }
                curtime = cooltime;
        }
        curtime -= Time.deltaTime;

        if (curtime <= 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Debug.Log("총알발사");
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
    }
}
