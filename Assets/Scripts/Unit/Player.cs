using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public float hp;
    public int ItemCount;
    private bool isInvincible = false;
    Animator anim;

    Rigidbody2D rigid;

    void Awake()
    {
        if (FindObjectsOfType<Player>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        speed = 13;
        hp = 6;
        DontDestroyOnLoad(this.gameObject);
        ItemCount = 0;
    }

    private void Update()
    {
        if (hp <= 0)
        {
            GameManager.instance.GameOver();
            Destroy(gameObject);
            Debug.Log("플레이어 사망");
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 3.5f;
        }
        else
        {
            speed = 6.5f;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("isRightMove", true);
            anim.SetBool("isLeftMove", false);
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("isLeftMove", true);
            anim.SetBool("isRightMove", false);
        }
        else
        {
            anim.SetBool("isRightMove", false);
            anim.SetBool("isLeftMove", false);
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 6.5f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 13;
        }
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            Debug.Log("아이템 휙득");
            ItemCount += 1;
            if(ItemCount > 4)
            {
                ItemCount -= 1;
            }
        }
        
        if (other.CompareTag("EnemyAttack") && !isInvincible)
        {
            Debug.Log("피격");
            hp -= 1;
            StartCoroutine(HitCooldown());
        }

        if (other.CompareTag("EnemyLaser") && !isInvincible)
        {
            Debug.Log("레이저 피격");
            hp -= 1;
            StartCoroutine(HitCooldown());
        }
    }

    IEnumerator HitCooldown()
    {
        isInvincible = true;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float blinkInterval = 0.1f;
        float timer = 0f;

        while (timer < 0.3f)
        {
            sr.color = new Color(1f, 1f, 1f, 0.3f);
            yield return new WaitForSeconds(blinkInterval);

            sr.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(blinkInterval);

            timer += blinkInterval * 2f;
        }

        isInvincible = false;
    }


    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
}
