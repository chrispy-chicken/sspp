using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleMonsterBossAI : MonoBehaviour
{
    PurpleMonsterCutscene pmc; // script reference
    public bool isActive = false;
    SpriteRenderer sr;
    private GameObject player;
    public Sprite[] sprites;
    Rigidbody2D rb;
    private float speed = 0.05f;
    private bool stopMoving = false;
    private int random;

    void Start()
    {
        pmc = GetComponent<PurpleMonsterCutscene>();
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

        if (pmc.isReady)
        {
            sr.flipX = true;
            pmc.Scream();
            // wait 2 seconds then set isActive to true
            StartCoroutine(WaitAndSet());
            
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void Update()
    {
        if (isActive)
        {
            CheckForFlip();
            Move();
        }
    }

    IEnumerator WaitAndSet()
    {
        yield return new WaitForSeconds(2);
        isActive = true;
    }

    private void CheckForFlip()
    {
        if (player.transform.position.x > transform.position.x)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }   

    private void OpenMouth()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[1];
    }

    private void CloseMouth()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[0];
    }
    private void Move()
    {
        if (stopMoving)
        {
            return;
        }

        random = Random.Range(1, 1000);
        if (random == 1)
        {
            speed = 0.2f;
            OpenMouth();
            StartCoroutine(CloseMouthAfterTime());
        }
        rb.MovePosition(Vector2.MoveTowards(transform.position, player.transform.position, speed));
    }

    IEnumerator CloseMouthAfterTime()
    {
        yield return new WaitForSeconds(3);

        speed = 0.05f;
        CloseMouth();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // disable collision between the player and the boss for a bit
        if (collision.gameObject.tag == "Player")
        {
            stopMoving = true;
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);
            player.GetComponent<PlayerMovement>().TakenDamageAndInvincible();
            StartCoroutine(EnableCollision());
        }
    }

    IEnumerator EnableCollision()
    {
        yield return new WaitForSeconds(2);
        stopMoving = false;
        yield return new WaitForSeconds(1);
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
    }
}
