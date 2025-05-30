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

    private List<GameObject> currentSubs = new();
    private bool initialized = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        StartCoroutine(WaitAndInitialize());
    }

    void Update()
    {
        if (!initialized || player == null || playerScript == null) return;

        int count = playerScript.ItemCount;
        bool isShift = Input.GetKey(KeyCode.LeftShift);

        if (count == lastCount && isShift == lastShift) return;

        if (count < 1)
            ClearSubs();
        else
            UpdateSubs(count, isShift);

        lastCount = count;
        lastShift = isShift;
    }

    IEnumerator WaitAndInitialize()
    {
        while (player == null || playerScript == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player != null)
                playerScript = player.GetComponent<Player>();

            yield return null;
        }

        while (player.transform.position == Vector3.zero)
            yield return null;

        initialized = true;

        int count = playerScript.ItemCount;
        bool isShift = Input.GetKey(KeyCode.LeftShift);

        if (count >= 1)
            UpdateSubs(count, isShift);

        lastCount = count;
        lastShift = isShift;
    }

    void UpdateSubs(int count, bool shift)
    {
        ClearSubs();

        Vector3[] baseOffsets = shift
            ? new[] {
                new Vector3(0f, 0.8f, 0f),
                new Vector3(-0.6f, 0f, 0f),
                new Vector3(0.6f, 0f, 0f),
                new Vector3(0f, -0.8f, 0f)
              }
            : new[] {
                new Vector3(0f, 1f, 0f),
                new Vector3(-1f, 0f, 0f),
                new Vector3(1f, 0f, 0f),
                new Vector3(0f, -1f, 0f)
              };

        for (int i = 0; i < count && i < subPrefabs.Length; i++)
        {
            Vector3 spawnPos = player.transform.position + baseOffsets[i];
            GameObject sub = Instantiate(subPrefabs[i], spawnPos, Quaternion.identity);

            if (sub.TryGetComponent(out FollowPlayer follow))
            {
                follow.target = player.transform;
                follow.offset = baseOffsets[i];
            }

            sub.transform.DOMove(spawnPos, 0.1f).SetEase(Ease.OutBounce);
            currentSubs.Add(sub);
        }
    }

    void ClearSubs()
    {
        foreach (var obj in currentSubs)
        {
            if (obj != null) Destroy(obj);
        }
        currentSubs.Clear();
    }
}
