using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sub_Attack : MonoBehaviour
{
    public GameObject bullet;     // 발사할 서브 총알 프리팹 (Sub_bullet이 붙어 있어야 함)
    public GameObject BigGun;     // 필요시 사용
    public GameObject Player;     // 플레이어 오브젝트
    public Transform pos;         // 총알 발사 위치 (Sub의 앞쪽 방향 기준)
    public float cooltime;        // 일반 공격 쿨타임
    public float Bigcooltime;     // (필요시 사용)
    private float curtime;
    private float Bigcurtime;

    private Player Icount;

    void Start()
    {
        Icount = Player.GetComponent<Player>();
    }

    void Update()
    {
        curtime -= Time.deltaTime;

        if (curtime <= 0)
        {
            bool zPressed = Input.GetKey(KeyCode.Z);
            bool shiftPressed = Input.GetKey(KeyCode.LeftShift);

            if (zPressed || shiftPressed)
            {
                Debug.Log("Sub 총알 발사");

                if (bullet != null)
                {
                    // 총알 생성
                    GameObject firedBullet = Instantiate(bullet, pos.position, pos.rotation);

                    // Rigidbody2D로 직선 발사
                    Rigidbody2D rb = firedBullet.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.velocity = pos.up * 10f; // pos의 "앞 방향"으로 발사 (10은 속도값, 필요시 public 변수로 빼도 됨)
                    }
                }

                curtime = cooltime;
            }
        }
    }
}
