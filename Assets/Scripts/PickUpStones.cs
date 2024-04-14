using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpStones : MonoBehaviour
{
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject stones;

    private bool ePressed = false;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null && ui != null)
        {
            ui.SetActive(true);
            StartCoroutine(WaitForInput());
        }  
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null && ui != null)
        {
            ui.SetActive(false);
            StopCoroutine(WaitForInput());
        }
    }

    IEnumerator WaitForInput()
    {
        while (ePressed == false)
        {
            ePressed = (Input.GetKeyDown(KeyCode.E)) ? true : false;
            yield return null;
        }
        Destroy(stones);
        
    }
}
