using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sub : MonoBehaviour
{
    private GameObject player;
    public GameObject sub1;
    public GameObject sub2;
    public GameObject sub3;
    public GameObject sub4;

    private Vector3 playerPos;
    private Player Icount;

    private int previousItemCount = 0;
    private bool shiftPressedLastFrame = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            playerPos = player.transform.position;
            Icount = player.GetComponent<Player>();
        }
        else
        {
            Debug.LogError("Player 오브젝트 어디갔노.");
        }
    }

    void FixedUpdate()
    {
        if (Icount == null) return;

        playerPos = player.transform.position;
        int currentItemCount = Icount.ItemCount;
        bool shiftPressed = Input.GetKey(KeyCode.LeftShift);

        if (currentItemCount != previousItemCount || shiftPressed != shiftPressedLastFrame)
        {
            SpawnSubs(currentItemCount, shiftPressed);

            previousItemCount = currentItemCount;
            shiftPressedLastFrame = shiftPressed;
        }
    }

    void SpawnSubs(int count, bool shift)
    {
        if (count == 1)
        {
            Instantiate(sub1, new Vector3(0, playerPos.y + 1, 0), Quaternion.identity);

            if (shift)
            Instantiate(sub1, new Vector3(0, playerPos.y + 0.8f, 0), Quaternion.identity);
        }
        else if (count == 2)
        {
            Instantiate(sub1, new Vector3(playerPos.x - 1, 0, 0), Quaternion.identity);
            Instantiate(sub2, new Vector3(playerPos.x + 1, 0, 0), Quaternion.identity);

            if (shift)
            {
                Instantiate(sub1, new Vector3(playerPos.x - 0.6f, 0, 0), Quaternion.identity);
                Instantiate(sub2, new Vector3(playerPos.x + 0.6f, 0, 0), Quaternion.identity);
            }
        }
        else if (count == 3)
        {
            Instantiate(sub1, new Vector3(playerPos.x - 1, 0, 0), Quaternion.identity);
            Instantiate(sub2, new Vector3(playerPos.x + 1, 0, 0), Quaternion.identity);
            Instantiate(sub3, new Vector3(0, playerPos.y + 1, 0), Quaternion.identity);

            if (shift)
            {
                Instantiate(sub1, new Vector3(playerPos.x - 0.6f, 0, 0), Quaternion.identity);
                Instantiate(sub2, new Vector3(playerPos.x + 0.6f, 0, 0), Quaternion.identity);
                Instantiate(sub3, new Vector3(0, playerPos.y + 0.8f, 0), Quaternion.identity);
            }
        }
        else if (count == 4)
        {
            Instantiate(sub1, new Vector3(playerPos.x - 1, 0, 0), Quaternion.identity);
            Instantiate(sub2, new Vector3(playerPos.x - 0.55f, playerPos.y + 1, 0), Quaternion.identity);
            Instantiate(sub3, new Vector3(playerPos.x + 0.55f, playerPos.y + 1, 0), Quaternion.identity);
            Instantiate(sub1, new Vector3(playerPos.x + 1, 0, 0), Quaternion.identity);

            if (shift)
            {
                Instantiate(sub1, new Vector3(playerPos.x - 0.6f, 0, 0), Quaternion.identity);
                Instantiate(sub2, new Vector3(playerPos.x - 0.4f, playerPos.y + 0.8f, 0), Quaternion.identity);
                Instantiate(sub3, new Vector3(playerPos.x + 0.4f, playerPos.y + 0.8f, 0), Quaternion.identity);
                Instantiate(sub1, new Vector3(playerPos.x + 0.6f, 0, 0), Quaternion.identity);
            }
        }
    }
}
