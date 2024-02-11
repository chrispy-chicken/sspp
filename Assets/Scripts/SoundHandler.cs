using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    private bool slide = false;
    private bool appears = false;
    GameObject music;

    public void PlaySliding()
    {
        if (slide)
        {
            return;
        }
        music = GameObject.FindGameObjectWithTag("slide");
        music.GetComponent<AudioSource>().Play();
        slide = true;
    }

    public void PlayAppears()
    {
        if (appears)
        {
            return;
        }
        music = GameObject.FindGameObjectWithTag("appears");
        music.GetComponent<AudioSource>().Play();
        appears = true;
    }

    public void StopSlidingSoundEffect()
    {
        music = GameObject.FindGameObjectWithTag("slide");
        music.GetComponent<AudioSource>().Stop();
    }

    public void PlayHeartFall()
    {
        music = GameObject.FindGameObjectWithTag("heart");
        music.GetComponent<AudioSource>().Play();
    }
}
