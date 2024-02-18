using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetbuttondiable : MonoBehaviour
{
    GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
            canvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // disbale canvas
            canvas.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        { 
            canvas.SetActive(true);
        }
    }
}
