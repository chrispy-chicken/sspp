using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampHandler : MonoBehaviour
{
    public Sprite[] spriteArray;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public void UpdateSprite(bool open)
    {
        if (open)
        {
            spriteRenderer.sprite = spriteArray[1];
        }
        else
        {
            spriteRenderer.sprite = spriteArray[0];
        }
    }
}
