using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    // coordinaten van plek waar je naartoe teleporteert
    private List<LampHandler> lampHandlers = new List<LampHandler>();
    public GameObject[] assignedLamps;

    public Vector3 targetLocation = new Vector3(0, 0, 0);
    public bool unlocked = false;
    public int assigned_plates = 1;
    private int plates_active = 0;

    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;

    public Animator transition;
    public float transitionTime = 0.2f;

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
            other.GetComponent<PlayerMovement>().checkPointPosition = targetLocation;
            StartCoroutine(Teleport(other));
        }
    }

    public void OpenTheNoorOrCloseTheNoorKutLangeNaamFunctieLol(bool open)
    {   
        if (spriteRenderer == null)
        {
            return;
        }

        if (open) {
            plates_active++;
        }
        else {
            plates_active--;
        }

        // sesam open
        if (plates_active == assigned_plates) 
        {
            unlocked = true;
            spriteRenderer.sprite = spriteArray[1];

            foreach (var lampHandler in lampHandlers)
            {
                lampHandler.UpdateSprite(true);
            }
        }

        // sesam dicht
        else 
        {
            unlocked = false;
            spriteRenderer.sprite = spriteArray[0]; 

            foreach (var lampHandler in lampHandlers)
            {
                lampHandler.UpdateSprite(false);
            }
        }


    }

    IEnumerator Teleport(Collider2D other)
    {
        other.GetComponent<PlayerMovement>().frozen = true;

        // begin de fade out
        transition.SetTrigger("Start");
        // effe wachten
        yield return new WaitForSeconds(transitionTime);
        // teleporteren
        other.transform.position = new Vector2 (targetLocation.x, targetLocation.y);

        other.GetComponent<PlayerMovement>().frozen = false;
    }
}
