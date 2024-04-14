using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBubble;
    [SerializeField] private Text text;
    [SerializeField] private string dialogue;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            if (dialogueBubble != null && text != null)
            {
                text.text = dialogue;
                text.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, text.preferredHeight);
                dialogueBubble.SetActive(true);
            }
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            if (dialogueBubble != null && text != null)
            {
                dialogueBubble.SetActive(false);
            }
        }
    }
}
