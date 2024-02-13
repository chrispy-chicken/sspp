using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHandleCollision : MonoBehaviour
{
    Rigidbody2D rb;
    private GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // get child object and set it to the location of the player mirrored through the center of the box
        transform.GetChild(0).position = 2 * (transform.position + new Vector3(0f, 0.5f,0f)) - player.transform.position + new Vector3(0f, 0.5f, 0f);

        transform.GetChild(0).position = transform.position + new Vector3(0f, 0.5f, 0f) + Mathf.Sign(transform.position.x - player.transform.position.x) * new Vector3(1f, 0f, 0f);
        transform.GetChild(1).position = transform.position + new Vector3(0f, 0.5f, 0f) + Mathf.Sign(transform.position.y - player.transform.position.y + 0.5f) * new Vector3(0f, 0.5f, 0f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "boxDot")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "boxDot")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
