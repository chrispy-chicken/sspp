using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    // coordinaten van plek waar je naartoe teleporteert
    public Vector3 targetLocation = new Vector3(0, 0, 0);
    public GameObject[] assignedLamps;
    private List<LampHandler> lampHandlers = new List<LampHandler>();

    public bool unlocked = false;
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;

    public Animator transition;
    public float transitionTime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        foreach (var assignedLamp in assignedLamps)
        {
            lampHandlers.Add(assignedLamp.GetComponent<LampHandler>());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.gameObject.tag == "Player" && unlocked)
        {
            Debug.Log("TELEPORT");

            StartCoroutine(Teleport(other));
            
        }
    }

    public void OpenTheNoorOrCloseTheNoorKutLangeNaamFunctieLol(bool open)
    {   
        if (spriteRenderer == null)
        {
            return;
        }
        // sesam open
        if (open) 
        {
            unlocked = true;
            spriteRenderer.sprite = spriteArray[1];
        }

        // sesam dicht
        else 
        {
            unlocked = false;
            spriteRenderer.sprite = spriteArray[0]; 
        }

        foreach (var lampHandler in lampHandlers)
        {
            lampHandler.UpdateSprite(open);
        }
    }

    IEnumerator Teleport(Collider2D other)
    {
        // begin de fade out
        transition.SetTrigger("Start");
        // effe wachten
        yield return new WaitForSeconds(transitionTime);
        // teleporteren
        other.transform.position = new Vector2 (targetLocation.x, targetLocation.y);
    }
}
