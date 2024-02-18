using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bossswitch : MonoBehaviour
{
    private SpriteRenderer sr;
    int numActive;
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((other.gameObject.tag == "Box" || other.gameObject.tag == "bossBox") && other.gameObject.GetComponent<SpriteRenderer>().color == Color.white) || other.gameObject.tag == "Player")
        {
            numActive++;
            player.GetComponent<PlayerMovement>().finalPlates++;
            if (numActive == 1)
            {
                sr.flipY = true;
                sr.flipX = true;

                // unlocking door, switching sprite
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Box" || other.gameObject.tag == "Player")
        {
            numActive--;
            player.GetComponent<PlayerMovement>().finalPlates--;
            if (numActive <= 0)
            {
                sr.flipX = false;
                sr.flipY = false;
            }
        }
    }

}
