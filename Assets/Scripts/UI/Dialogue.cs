using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBubble;
    [SerializeField] private TMP_Text TMP_text;
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
            if (dialogueBubble != null && TMP_text != null)
            {
                if (player) { TMP_text.color = Color.black; }
                else if (hand) { TMP_text.color = Color.red; }
                else if (water) { TMP_text.color = Color.blue; }
                else if (barrier) { TMP_text.color = Color.green; }
                else { TMP_text.color = Color.black; }
                TMP_text.text = dialogue;
                TMP_text.rectTransform.sizeDelta = new Vector2(TMP_text.rectTransform.sizeDelta.x, TMP_text.preferredHeight);
                dialogueBubble.SetActive(true);
            }
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            if (dialogueBubble != null && TMP_text != null)
            {
                dialogueBubble.SetActive(false);
            }
        }
    }
}
