using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int health;
    [SerializeField] private GameObject[] hearts;
    GameObject heart;
    SoundHandler sh;

    void Start()
    {
        health = 4;
    }

    void Update()
    {
        if (health == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu 1");
        }
        foreach (GameObject heart in hearts)
        {
            if (heart.transform.position.y < -100)
            {
                heart.GetComponent<Rigidbody2D>().gravityScale = 0;
            }
        }
    }

    public void GotHit()
    {
        health--;
        try
        {
            heart = hearts[health];
            heart.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        catch (System.IndexOutOfRangeException)
        {
            Debug.Log("This is not supposed to happen");
        }
        sh = GetComponent<SoundHandler>();
        sh.PlayHeartFall();

    }

}
