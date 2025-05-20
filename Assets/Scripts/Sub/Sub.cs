using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Sub : MonoBehaviour
{
    public GameObject[] subPrefabs;
    private GameObject player;
    private Player playerScript;

    private int lastCount = -1;
    private bool lastShift = false;

    private List<GameObject> currentSubs = new List<GameObject>();

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerScript = player.GetComponent<Player>();

            int count = playerScript.ItemCount;
            bool isShift = Input.GetKey(KeyCode.LeftShift);
            UpdateSubs(count, isShift);

            lastCount = count;
            lastShift = isShift;
        }
        else
        {
            Debug.LogError("Player 어디갔노");
        }
    }


    void FixedUpdate()
    {
        if (playerScript == null) return;

        int count = playerScript.ItemCount;
        bool isShift = Input.GetKey(KeyCode.LeftShift);

        if (count != lastCount || isShift != lastShift)
        {
            UpdateSubs(count, isShift);
            lastCount = count;
            lastShift = isShift;
        }
    }

    void UpdateSubs(int count, bool shift)
    {
        // 기존 sub들 제거
        foreach (GameObject obj in currentSubs)
        {
            Destroy(obj);
        }
        currentSubs.Clear();

        Vector3[] offsets = new Vector3[4];

        if (shift)
        {
            offsets[0] = new Vector3(0f, 0.8f, 0f);
            offsets[1] = new Vector3(-0.6f, 0f, 0f);
            offsets[2] = new Vector3(0.6f, 0f, 0f);
            offsets[3] = new Vector3(0f, -0.8f, 0f);
        }
        else
        {
            offsets[0] = new Vector3(0f, 1f, 0f);
            offsets[1] = new Vector3(-1f, 0f, 0f);
            offsets[2] = new Vector3(1f, 0f, 0f);
            offsets[3] = new Vector3(0f, -1f, 0f);
        }

        // sub 생성 및 설정
        for (int i = 0; i < count && i < subPrefabs.Length; i++)
        {
            Vector3 spawnPos = player.transform.position + offsets[i];
            GameObject sub = Instantiate(subPrefabs[i], spawnPos, Quaternion.identity);

            FollowPlayer follow = sub.GetComponent<FollowPlayer>();
            if (follow != null)
            {
                follow.target = player.transform;
                follow.offset = offsets[i];
            }

            sub.transform.DOMove(spawnPos, 0.1f).SetEase(Ease.OutBounce);
            currentSubs.Add(sub);
        }
    }
}
