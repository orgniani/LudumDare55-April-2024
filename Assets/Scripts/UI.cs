using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [Header("References to the spell images.")]
    [SerializeField] private Image hand;
    [SerializeField] private Image water;
    [SerializeField] private Image barrier;
    
    private MainManager manager;

    private void Awake()
    {
        manager = FindAnyObjectByType<MainManager>();
    }
    void Start()
    {
        StartCoroutine(ShowSpellUIWhenStonesCollected());
    }

    private void Update()
    {
        hand.color = manager.inHandZone ? Color.white : Color.gray;
        water.color = manager.inWaterZone ? Color.white : Color.gray;
        barrier.color = manager.inBarrierZone ? Color.white : Color.gray;
    }

    IEnumerator ShowSpellUIWhenStonesCollected()
    {
        yield return new WaitUntil(() => manager.hasStones);
        if (hand != null)
        {
            hand.gameObject.SetActive(true);
        }
        if (water != null)
        {
            water.gameObject.SetActive(true);
        }
        if (barrier != null)
        {
            barrier.gameObject.SetActive(true);
        }
        yield break;
    }
}
