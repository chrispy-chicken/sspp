using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossCutsceneTrigger : MonoBehaviour
{
    private GameObject player;
    private GameObject finalBoss;
    public bool startBossMoving = false;
    bool done = false;
    bool stopUpdating = false;

    private GameObject[] boxes;
    GameObject[] text;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        finalBoss = GameObject.FindGameObjectWithTag("enemy");
        text = GameObject.FindGameObjectsWithTag("WhatHaveYouDone?");
        boxes = GameObject.FindGameObjectsWithTag("bossBox");
        for (int i = 0; i < text.Length; i++)
        {
            text[i].SetActive(false);
        }
    }

    private void FixedUpdate()
    {

        if (stopUpdating)
        {
            return;
        }

        if (finalBoss.transform.position.y > 117.5f && startBossMoving)
        {
            finalBoss.transform.position = new Vector3(finalBoss.transform.position.x, finalBoss.transform.position.y - 0.1f, finalBoss.transform.position.z);
        }
        else if (finalBoss.transform.position.y <= 117.5f && startBossMoving)
        {
            player.GetComponent<PlayerMovement>().enabled = true;
            stopUpdating = true;
            
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // stop playermovement
        if (!done)
        {
            done = true;
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<Animator>().SetBool("moving", false);
            //trigger dialogue
            // wow
            // such dialogue

            StartCoroutine(StartFinalBossCutscene());
        }
    }

    IEnumerator StartFinalBossCutscene()
    {
        for (int i = 0; i < text.Length; i++)
        {
            text[i].SetActive(true);
        }
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < text.Length; i++)
        {
            text[i].SetActive(false);
        }
        startBossMoving = true;
    }
}
