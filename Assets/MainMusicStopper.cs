using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMusicStopper : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<AudioSource>().Stop();
        }
    }
}
