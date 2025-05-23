using UnityEngine;

public class BackGround1 : MonoBehaviour
{
    public float speed = 2f;
    public Transform startBackground; // Start 배경
    public Transform[] repeatBackgrounds; // 반복 배경들

    private float startHeight;
    private float repeatHeight;
    private bool isStartScrolling = true;

    void Start()
    {
        if (repeatBackgrounds.Length == 0 || startBackground == null) return;

        startHeight = startBackground.GetComponent<SpriteRenderer>().bounds.size.y;
        repeatHeight = repeatBackgrounds[0].GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        if (isStartScrolling)
        {
            startBackground.Translate(Vector3.down * speed * Time.deltaTime);

            if (startBackground.position.y < -startHeight)
            {
                isStartScrolling = false;
            }
        }
        for (int i = 0; i < repeatBackgrounds.Length; i++)
        {
            repeatBackgrounds[i].Translate(Vector3.down * speed * Time.deltaTime);

            if (repeatBackgrounds[i].position.y < -repeatHeight)
            {
                float highestY = GetHighestRepeatY();
                repeatBackgrounds[i].position = new Vector3(repeatBackgrounds[i].position.x, highestY + repeatHeight, repeatBackgrounds[i].position.z);
            }
        }
    }

    float GetHighestRepeatY()
    {
        float highestY = repeatBackgrounds[0].position.y;
        for (int i = 1; i < repeatBackgrounds.Length; i++)
        {
            if (repeatBackgrounds[i].position.y > highestY)
            {
                highestY = repeatBackgrounds[i].position.y;
            }
        }
        return highestY;
    }
}
