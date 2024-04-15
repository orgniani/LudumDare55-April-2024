using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    [Header("How far away from the player the hand spawns")]
    [SerializeField] private float xOffset = 0.0f;
    [SerializeField] private float yOffset = 0.0f;
    [SerializeField] private float zOffset = 0.0f;

    [Space(10)]
    [Header("How many seconds the hand remains on screen")]
    [SerializeField] private float activeTime = 10.0f;
    private float time = 0.0f;

    private PlayerController player;
    private Vector3 playerPosition;
    private Vector3 spawnPosition;


    private void Awake()
    {
        player = FindAnyObjectByType<PlayerController>();
    }

    private void OnEnable()
    {
        playerPosition = player.gameObject.transform.position;
        spawnPosition = new Vector3(playerPosition.x + xOffset, playerPosition.y + yOffset, playerPosition.z + zOffset);
        gameObject.transform.position = spawnPosition;
        StartCoroutine(InactivateAfterTime());
    }

    private IEnumerator InactivateAfterTime()
    {
        while (time < activeTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
        time = 0.0f;
        Deactivate();
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
