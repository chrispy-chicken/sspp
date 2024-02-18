using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PurpleMonsterCutscene : MonoBehaviour
{
    private bool isAwake = false;
    private bool startColoring = false;
    public bool isReady;
    private bool playIntroFlips = false;
    private bool stopper = false;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    public Sprite[] sprites;
    PurpleMonsterBossAI pmai;
    SoundHandler sh;
    //private Animator animator; // fuck animating

    GameObject[] boxes;
    void Start()
    {

        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        // animator = GetComponent<Animator>();
        pmai = GetComponent<PurpleMonsterBossAI>();
        sh = GetComponent<SoundHandler>();
        boxes = GameObject.FindGameObjectsWithTag("bossBox");

        if (!isReady)
        {
            // set color to black
            sr.color = new Color(0, 0, 0, 1);
            // set Freeze Position X in the constraints of the Rigidbody2D component to false
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

            transform.position = new Vector3(-107.13f, 130.9f, 0f); // hardcode starting position 
        }
        else
        {
            // this code should not run
            transform.position = new Vector3(-9.29f, 1f, 0.2f); // starting position
        }

        // for each box, ignore collision with enemy. Physics2D.IgnoreCollision(box.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        for (int i = 0; i < boxes.Length; i++)
        {
            Physics2D.IgnoreCollision(boxes[i].GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

    }

    void FixedUpdate()
    {
        if (transform.position.x < -108f && !isReady)
        {
            sh.PlaySliding();
        }

        if (!isReady && !pmai.isActive) { 
            if (transform.position.x < -114f && !isAwake && !stopper) 
            {
                sh.StopSlidingSoundEffect();
                isAwake = true;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            if (isAwake && !startColoring && !playIntroFlips)
            {
                // wait a bit then lerp the color to white
                StartCoroutine(WaitAndLerp());
                stopper = true; // idk man spaghetti code, if you remove this it breaks
                isAwake = false;
            }

            if (startColoring)
            {
                sr.color = Color.Lerp(sr.color, new Color(1, 1, 1, 1), 0.04f); //Solved? //TODO appears to be a magic number, 0.01 is what works in the unity editor, but 0.04 is what works in the build
                sh.PlayAppears();
                if (sr.color == new Color(1, 1, 1, 1))
                {
                    startColoring = false;
                    playIntroFlips = true;
                }
            }

            if (playIntroFlips)
            {
                // flip the sprite over the x axis, wait a bit, then flip it back
                StartCoroutine(FlipSprite());
                playIntroFlips = false;
                
            }
        }
    }

    IEnumerator WaitAndLerp()
    {
        yield return new WaitForSeconds(3f);
        startColoring = true;
    }

    IEnumerator FlipSprite()
    {
        sr.flipX = true;
        yield return new WaitForSeconds(0.5f);
        sr.flipX = false;
        yield return new WaitForSeconds(0.5f);
        sr.flipX = true;
        yield return new WaitForSeconds(0.5f);
        sr.flipX = false;
        yield return new WaitForSeconds(0.5f);
        sr.flipX = true;
        yield return new WaitForSeconds(1f);

        Scream();
        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].GetComponent<MoveBossBoxes>().moveToNode = true;
        }
        playIntroFlips = false;
    }

    public void Scream()
    {
        ChangeSprite(2);
        GetComponent<AudioSource>().Play();
        PlayBossMusic();
        StartCoroutine(WaitAndSetSprite());
    }

    IEnumerator WaitAndSetSprite()
    {
        yield return new WaitForSeconds(1.5f);
        ChangeSprite(0);
        sr.flipX = true;
        
    }

    void PlayBossMusic()
    {
        // play the boss music
        GameObject music;
        music = GameObject.FindGameObjectWithTag("music");
        music.GetComponent<AudioSource>().Play();
        isReady = true;

        // wait 2 seconds then set isActive to true
        StartCoroutine(WaitAndSet());
    }

    void ChangeSprite(int spriteIndex)
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[spriteIndex];
    }

    IEnumerator WaitAndSet()
    {
        yield return new WaitForSeconds(3);
        pmai.isActive = true;
    }
}
