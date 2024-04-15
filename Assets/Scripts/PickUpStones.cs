using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpStones : MonoBehaviour
{
    [SerializeField] private GameObject stonesUI;
    [SerializeField] private GameObject stones;
    [SerializeField] private GameObject earthquake;

    private MainManager mainManager;
    private bool ePressed = false;

    private void Awake()
    {
        mainManager = FindAnyObjectByType<MainManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        /*PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null && stonesUI != null)
        {
            stonesUI.SetActive(true);
            StartCoroutine(WaitForInput());
        }  */
    }

    private void OnTriggerExit(Collider other)
    {
       /* PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null && stonesUI != null)
        {
            stonesUI.SetActive(false);
            StopCoroutine(WaitForInput());
        }*/
    }

    IEnumerator WaitForInput()
    {
        while (ePressed == false)
        {
            ePressed = (Input.GetKeyDown(KeyCode.E)) ? true : false;
            yield return null;
        }
        Destroy(stones);
        if (mainManager != null) { mainManager.hasStones = true; }
        if (earthquake != null) { earthquake.SetActive(true); }
        
    }
}
