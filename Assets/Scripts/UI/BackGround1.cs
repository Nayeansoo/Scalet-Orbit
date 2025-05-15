using UnityEngine;

public class BackGround1 : MonoBehaviour
{
    public float speed = 2f;
    public Transform[] sprites;

    private float spriteHeight;

    void Start()
    {
        if (sprites.Length == 0) return;

        spriteHeight = sprites[0].GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].Translate(Vector3.down * speed * Time.deltaTime);

            if (sprites[i].position.y < -spriteHeight)
            {
                float highestY = GetHighestSpriteY();

                sprites[i].position = new Vector3(sprites[i].position.x, highestY + spriteHeight, sprites[i].position.z);
            }
        }
    }
    float GetHighestSpriteY()
    {
        float highestY = sprites[0].position.y;
        for (int i = 1; i < sprites.Length; i++)
        {
            if (sprites[i].position.y > highestY)
            {
                highestY = sprites[i].position.y;
            }
        }
        return highestY;
    }
}
