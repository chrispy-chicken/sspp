using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBossBoxes : MonoBehaviour
{
    public Transform node;
    public bool moveToNode = false;
    void Start()
    {
        moveToNode = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    void FixedUpdate()
    {
        /*if (!onShelf)
        {
            if (transform.position.x < -9f || transform.position.x > 32f || transform.position.y > 38.9f || transform.position.y < 25.4f)
            {
                moveToNode = true;
            }
        }*/

        if (moveToNode)
        {
            transform.position = Vector3.MoveTowards(transform.position, node.position, 0.07f);
        }

        // if transform.position is close enough to node.position, stop moving
        if (Vector3.Distance(transform.position, node.position) < 0.1f)
        {
            moveToNode = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }

    }

}
