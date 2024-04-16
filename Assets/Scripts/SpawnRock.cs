using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRock : MonoBehaviour
{
    public GameObject Rock;
    public bool stopRock;

    void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            Rock.SetActive(true);
        }
    }
}
