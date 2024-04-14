using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandBehaviour : MonoBehaviour
{
    [Header ("How far away from the player the hand spawns")]
    [SerializeField] private float xOffset = 2.69f;
    [SerializeField] private float yOffset = -16.12f;
    [SerializeField] private float zOffset = 7.28f;
    
    [Space (10)]
    [Header("How many seconds the hand remains on screen")]
    [SerializeField] private float activeTime = 20.0f;
    private float time = 0.0f;

    private PlayerController player;
    private Animator animator;
    private Vector3 playerPosition;
    private Vector3 spawnPosition;

    
    private void Awake()
    {
        player = FindAnyObjectByType<PlayerController>(); 
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerPosition = player.gameObject.transform.position;
        spawnPosition = new Vector3 (playerPosition.x + xOffset , playerPosition.y + yOffset, playerPosition.z + zOffset);
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
        animator.Play("Dissapear");
        yield break;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    
    
}
