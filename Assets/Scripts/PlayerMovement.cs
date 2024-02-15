using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 0.1f;
    private Rigidbody2D rb;
    public Vector3 speedVector;
    private Animator animator;
    private SpriteRenderer sr;
    PlayerHealth playerHealth;
    public int maxStamina = 100;
    private int stamina;
    private bool needToRecover = false;
    

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
}
