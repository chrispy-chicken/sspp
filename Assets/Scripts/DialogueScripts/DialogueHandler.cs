using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpeechBubble
{
    [TextArea(3, 10)]
    public string Text;
}


public class DialogueHandler : MonoBehaviour
{
    public GameObject TargetSpeaker;
    public GameObject SpeechBubble;
    private Transform BubbleContainer;

    private Vector3 speechBubbleOffset = new Vector3(0f, 100f, 0f);
    public float bubbleDelay = 2.0f;
    private bool not_called = true;

    void Start()
    {
        BubbleContainer = GameObject.FindGameObjectWithTag("DialogueCanvas").transform.GetChild(0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.gameObject == TargetSpeaker && not_called)
        {
            foreach (Transform child in BubbleContainer) 
            {
                GameObject.Destroy(child.gameObject);
            }
            GameObject speechBubble = Instantiate(SpeechBubble, BubbleContainer);
            Vector3 player_pos = Camera.main.WorldToScreenPoint(TargetSpeaker.transform.position);
            BubbleContainer.position = player_pos + speechBubbleOffset;

            not_called = false;
            StartCoroutine(Destroy_after_delay(speechBubble));
        }
    }

    IEnumerator Destroy_after_delay(GameObject speechBubble)
    {
        Debug.Log($"yes! {speechBubble}");
        yield return new WaitForSeconds(bubbleDelay);

        Destroy(speechBubble);
    }
}
