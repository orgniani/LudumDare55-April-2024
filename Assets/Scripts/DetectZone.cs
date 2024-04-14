using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectZone : MonoBehaviour
{
    [Header ("The kind of spells that can be cast from this zone.") ]
    [SerializeField] private bool handZone;
    [SerializeField] private bool waterZone;
    [SerializeField] private bool barrierZone;

    private MainManager manager;

    private void Awake()
    {
        manager = FindAnyObjectByType<MainManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            if (handZone) { manager.inHandZone = true; }
            if (waterZone) { manager.inWaterZone = true; }
            if (barrierZone) { manager.inBarrierZone = true; }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            if (handZone) { manager.inHandZone = false; }
            if (waterZone) { manager.inWaterZone = false; }
            if (barrierZone) { manager.inBarrierZone = false; }
        }
    }
}
