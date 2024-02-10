using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 0.1f;
    private Rigidbody2D rb;
    private Vector3 speedVector;
    private Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // 2d sprite movement with the wasd keys
        speedVector = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);
        if (speedVector != Vector3.zero)
        {

            MovePlayer();
            animator.SetFloat("moveX", speedVector.x);
            animator.SetFloat("moveY", speedVector.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);           
        }
    }

    void MovePlayer()
    {
        rb.MovePosition(transform.position + speedVector * speed); // * Time.deltaTime); // idk?
    }
}
