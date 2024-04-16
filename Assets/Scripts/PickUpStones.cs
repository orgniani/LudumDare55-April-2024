using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpStones : MonoBehaviour
{
    [SerializeField] private GameObject stonesUI;
    [SerializeField] private GameObject stones;
    [SerializeField] private GameObject earthquake;
    [SerializeField] private PlayerManager playerManager;

    [SerializeField] private GameObject redStone;
    [SerializeField] private GameObject greenStone;
    [SerializeField] private GameObject blueStone;

    private bool ePressed = false;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null && stonesUI != null)
        {
            StartCoroutine(WaitForInput());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null && stonesUI != null)
        {
            stonesUI.SetActive(false);
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

        redStone.SetActive(true);
        greenStone.SetActive(true);
        blueStone.SetActive(true);
        stonesUI.SetActive(true);

        if (playerManager != null) { playerManager.SetPlayerHasSpiritStones(true); }
        if (earthquake != null) { earthquake.SetActive(true); }
        
    }
}
