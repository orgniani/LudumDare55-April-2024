using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBubble;
    [SerializeField] private Text text;
    [SerializeField] private string dialogue;
    [SerializeField] private bool player;
    [SerializeField] private bool hand;
    [SerializeField] private bool water;
    [SerializeField] private bool barrier;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            if (dialogueBubble != null && text != null)
            {
                if (player) { text.color = Color.black; }
                else if (hand) { text.color = Color.red; }
                else if (water) { text.color = Color.blue; }
                else if (barrier) { text.color = Color.green; }
                else { text.color = Color.black; }
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
