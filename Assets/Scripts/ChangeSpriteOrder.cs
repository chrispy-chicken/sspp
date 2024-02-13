using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteOrder : MonoBehaviour
{
    SpriteRenderer sr;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y-1f > transform.position.y)
        {
            // set sr order in layer to 3
            sr.sortingOrder = 3;
        }
        else
        {
            // set sr order in layer to 1
            sr.sortingOrder = 0;
        }
    }
}
