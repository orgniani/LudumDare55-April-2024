using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBoy : MonoBehaviour
{
    [SerializeField] private Material waterMaterial;
    [SerializeField] private Material iceMaterial;
    [SerializeField] private GameObject tail;

    [SerializeField] private float waitToFreeze;

    private Collider coll;
    private Renderer render;

    private void Awake()
    {
        render = GetComponent<Renderer>();
        coll = GetComponent<Collider>();
        coll.enabled = false;

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
        tail.SetActive(true);

        yield return new WaitForSeconds(waitToFreeze);

        coll.enabled = true;
        render.material = iceMaterial;
    }

}
