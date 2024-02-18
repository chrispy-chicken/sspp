using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForBox : MonoBehaviour
{
    private SpriteRenderer sr;
    private AudioSource audioSource;
    public GameObject assignedDoor;
    private DoorHandler doorHandler;
    private int numActive = 0;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        doorHandler = assignedDoor.GetComponent<DoorHandler>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((other.gameObject.tag == "Box" || other.gameObject.tag == "bossBox") && other.gameObject.GetComponent<SpriteRenderer>().color == Color.white) || other.gameObject.tag == "Player")
        {
            numActive++;
            
            if (numActive == 1)
            {
                sr.flipY = true;
                sr.flipX = true;
                audioSource.pitch = 0.7f;
                audioSource.Play();

                // unlocking door, switching sprite
                doorHandler.OpenTheNoorOrCloseTheNoorKutLangeNaamFunctieLol(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Box" || other.gameObject.tag == "Player")
        {
            numActive--;

            if (numActive <= 0)
            {
                sr.flipX = false;
                sr.flipY = false;
                // play the audio source sound with 0.8 pitch
                audioSource.pitch = 0.6f;
                audioSource.Play();

                // locking door, switching sprite
                doorHandler.OpenTheNoorOrCloseTheNoorKutLangeNaamFunctieLol(false);
            }
        }
    }
}
