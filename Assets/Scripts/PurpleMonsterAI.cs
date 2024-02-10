using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PurpleMonsterAI : MonoBehaviour
{
    public bool isAwake = false;
    private bool startColoring = false;
    public bool isReady = false;
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (!isReady)
        {
            // set color to black
            sr.color = new Color(0, 0, 0, 1);
            // set Freeze Position X in the constraints of the Rigidbody2D component to false
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

            transform.position = new Vector3(5.5f, 1f, 0.2f); // hardcode starting position 
        }
        else
        {
            transform.position = new Vector3(-9.29f, 1f, 0.2f); // starting position
        }
    }

    void Update()
    {
        if (!isReady) { 
            if (transform.position.x < -9.29f && !isAwake) // -9.29f is the portal like ground in the level
            {
                isAwake = true;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;

            }
            if (isAwake && !startColoring && !isReady)
            {
                // wait a bit then lerp the color to white
                StartCoroutine(WaitAndLerp());
            }

            if (startColoring)
            {
                sr.color = Color.Lerp(sr.color, new Color(1, 1, 1, 1), 0.01f);

                if (sr.color == new Color(1, 1, 1, 1))
                {
                    startColoring = false;
                    isReady = true;
                }
            }
        }
    }

    IEnumerator WaitAndLerp()
    {
        yield return new WaitForSeconds(5f);
        startColoring = true;
    }
}
