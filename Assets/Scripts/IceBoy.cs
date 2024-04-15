using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBoy : MonoBehaviour
{
    [SerializeField] private Material waterMaterial;
    [SerializeField] private Material iceMaterial;

    [SerializeField] private float waitToFreeze;

    private Collider coll;
    private Renderer render;
    private bool waterHit = false;

    private void Awake()
    {
        render = GetComponent<Renderer>();
        coll = GetComponent<Collider>();
        coll.isTrigger = true;

        if (!render)
        {
            Debug.LogError("Renderer component not found on the GameObject.");
        }

        else render.material = waterMaterial;
    }


    [ContextMenu("Iced!")]
    private void FreezeWater()
    {
        StartCoroutine(WaitToFreeze());
    }

    private IEnumerator WaitToFreeze()
    {
        yield return new WaitForSeconds(waitToFreeze);

        coll.isTrigger = false;
        render.material = iceMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DemonTail") && !waterHit)
        {
            waterHit = true;
            FreezeWater();
        }
    }
}
