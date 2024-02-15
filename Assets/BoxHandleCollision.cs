using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHandleCollision : MonoBehaviour
{
    Rigidbody2D rb;
    private GameObject player;
    Vector2 lastVelocity;
    List<Vector3> offsets;
    List<Vector3> horizontal;
    List<Vector3> vertical;
    Vector3 playerSpeed;
    float rayLength = 0.5f;
    public bool hitBox;
    Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        lastVelocity = new Vector2(0, 0);
        direction = new Vector2(0, 0);

        horizontal = new List<Vector3>
        {
            new Vector3(0, 0.3f, 0),          // Center
            //new Vector3(0.4f, 0.3f, 0),       // Right
            //new Vector3(-0.4f, 0.3f, 0),      // Left
            new Vector3(0, 0.6f -0.01f, 0),       // Up
            new Vector3(0, 0f + 0.01f, 0)       // Down
        };

        vertical = new List<Vector3>
        {
            new Vector3(0, 0.35f, 0),          // Center
            new Vector3(0.38f, 0.35f, 0),       // Right
            new Vector3(-0.38f, 0.35f, 0),      // Left
            //new Vector3(0, 0.6f -0.01f, 0),       // Up
            //new Vector3(0, 0f + 0.01f, 0)       // Down
        };
    }

    private void FixedUpdate()
    {
        float r = 1.1f;
        // if player is in radius 1 of the box, set rb.constraints to freeze rotation else freeze all
        if (Vector2.Distance(player.transform.position, transform.position + new Vector3(0f,0.7f,0f)) < r)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        //draw a sphere around the box to show the radius
        Debug.DrawLine(transform.position + new Vector3(0f, 0.7f, 0f), transform.position + new Vector3(0f, 0.4f, 0f) + new Vector3(r, 0, 0), Color.red);
        Debug.DrawLine(transform.position + new Vector3(0f, 0.7f, 0f), transform.position + new Vector3(0f, 0.4f, 0f) + new Vector3(-r, 0, 0), Color.red);
        Debug.DrawLine(transform.position + new Vector3(0f, 0.7f, 0f), transform.position + new Vector3(0f, 0.4f, 0f) + new Vector3(0, r, 0), Color.red);
        Debug.DrawLine(transform.position + new Vector3(0f, 0.7f, 0f), transform.position + new Vector3(0f, 0.4f, 0f) + new Vector3(0, -r, 0), Color.red);


        return;

        if (rb.velocity != Vector2.zero)
        {
            lastVelocity = rb.velocity;
        }
        hitBox = false;

        playerSpeed = player.GetComponent<PlayerMovement>().speedVector;


        /*if (playerSpeed.x != 0 && playerSpeed.y != 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            return;
        }
        else */ // check for diagonal movement
        if (playerSpeed.x != 0 && playerSpeed.y == 0)
        {
            offsets = horizontal;
            rayLength = 0.45f;
            direction = new Vector2(playerSpeed.x, 0f);
        }
        else if (playerSpeed.y != 0 && playerSpeed.x == 0)
        {
            offsets = vertical;
            rayLength = 0.4f;
            direction = new Vector2(0f, playerSpeed.y);
        }
        else if (playerSpeed == Vector3.zero && rb.velocity != Vector2.zero)
        {
            
        }
        else
        {
            return;
        }


        foreach (var offset in offsets)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + offset, direction.normalized, rayLength);
            //draw the raycasts to the screen
            Debug.DrawRay(transform.position + offset, direction.normalized * rayLength, Color.red);

            foreach (var hit in hits)
            {
                if (hit.collider != null && hit.collider.CompareTag("Box") && hit.collider.gameObject != gameObject)
                {
                    Debug.Log((gameObject, hit.collider.gameObject));
                    hitBox = true;

                    // draw a dot on the location of the hit
                    Debug.DrawRay(hit.point, Vector3.up, Color.green);

                    break;
                }
            }
        }

        if (hitBox)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
