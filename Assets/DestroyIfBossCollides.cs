using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfBossCollides : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject);
            StartCoroutine(DestroyAfterSeconds(0.5f));
        }
    }

    IEnumerator DestroyAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
