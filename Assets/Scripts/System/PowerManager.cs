using System.Collections;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
    private Player player;

    public GameObject Po1Prefab, Po2Prefab, Po3Prefab, Po4Prefab;

    private GameObject[] powerObjects = new GameObject[4];
    private Vector3[] positions = new Vector3[]
    {
        new Vector3(5.991f, -2.668f, 0),
        new Vector3(6.724f, -2.696f, 0),
        new Vector3(6.71f, -3.41f, 0),
        new Vector3(5.97f, -3.39f, 0)
    };

    void Start()
    {
        StartCoroutine(Initialize());
    }

    IEnumerator Initialize()
    {
        while (GameManager.instance == null || GameManager.instance.GetPlayer() == null)
        {
            yield return null;
        }

        player = GameManager.instance.GetPlayer();

        GameObject[] prefabs = { Po1Prefab, Po2Prefab, Po3Prefab, Po4Prefab };

        for (int i = 0; i < 4; i++)
        {
            powerObjects[i] = Instantiate(prefabs[i], positions[i], Quaternion.identity);
            powerObjects[i].SetActive(false);
            DontDestroyOnLoad(powerObjects[i]);
        }
    }

    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.GetPlayer() != null)
            player = GameManager.instance.GetPlayer();

        if (player == null) return;

        for (int i = 0; i < 4; i++)
        {
            if (powerObjects[i] == null) continue;
            powerObjects[i].SetActive(player.ItemCount > i);
        }
    }
}
