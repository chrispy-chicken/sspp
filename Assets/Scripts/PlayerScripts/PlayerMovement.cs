using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 0.1f;
    private Rigidbody2D rb;
    public Vector3 speedVector;
    private Animator animator;
    public Animator transition;
    private SpriteRenderer sr;
    PlayerHealth playerHealth;
    public int maxStamina = 100;
    private int stamina;
    private bool needToRecover = false;
    public bool frozen = false;
    
    public Vector3 checkPointPosition = new Vector3(6f, -19f, 0);

    GameObject staminaBar;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        playerHealth = GetComponent<PlayerHealth>();
        staminaBar = GameObject.FindGameObjectWithTag("Stamina");
        stamina = maxStamina;
    }

    void FixedUpdate()
    {
        // 2d sprite movement with the wasd keys
        speedVector = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f).normalized;
        
        if (speedVector != Vector3.zero && !frozen)
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

        if (Input.GetKey(KeyCode.Space) && !needToRecover)
        {
            speed = 0.2f;
            if (stamina > 0)
            {
                stamina--;
            }
        }
        else
        {
            speed = 0.1f;
            if (stamina < maxStamina)
            {
                stamina++;
            }
            if (stamina >= 9.0f / 10.0f * maxStamina)
            {
                needToRecover = false;
            }
        }

        staminaBar.transform.localScale = new Vector3(stamina * 0.025f, 2f, 1);

        if (stamina == 0)
        {
            needToRecover = true;
        }
    }

    void MovePlayer()
    {
        rb.MovePosition(transform.position + speedVector * speed); // * Time.deltaTime); // idk?
    }

    public void TakenDamageAndInvincible()
    {
        // when called, let the player sprite flicker for a few seconds
        playerHealth.GotHit();
        StartCoroutine(Invincible());
    }

    IEnumerator Invincible()
    {
        int count = 0;
        while (count < 10)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.1f);
            count++;
        }
    }

    void OnCollisionEnter2D (Collision2D other) 
	{   
        if (other.gameObject.tag == "Box")
        {
            animator.SetBool("pushing", true);
        }
	}

    void OnCollisionExit2D (Collision2D other) 
	{
		if (other.gameObject.tag == "Box")
        {
            animator.SetBool("pushing", false);
        }
	}

    public void ResetRooms()
    {   
        // when called, all boxes will be reset and the player will be teleported to the start of the room
        StartCoroutine(ResetPlayer());
    }

    IEnumerator ResetPlayer()
    {
        // begin de fade out
        transition.SetTrigger("Start");
        // effe wachten
        yield return new WaitForSeconds(1);
        
        // Resetten
        GameObject[] boxList = GameObject.FindGameObjectsWithTag("Box");
        
        foreach (var box in boxList)
        {
            box.transform.position = box.GetComponent<BoxHandleCollision>().originalPosition;
        }
        transform.position = checkPointPosition;
    }
}
