using System.Collections;
using UnityEngine;

public class FreezeEffect : MonoBehaviour
{
    [SerializeField] private Material waterMaterial;
    [SerializeField] private Material iceMaterial;

    private BoxCollider coll;
    private Renderer render;

    private void Awake()
    {
        render = GetComponent<Renderer>();
        coll = GetComponent<BoxCollider>();
        coll.enabled = false;

        if (!render)
        {
            DisableScriptIfMissingComponent("Renderer ('Renderer')");
            return;
        }

        render.material = waterMaterial;
    }

    public void FreezeWater()
    {
        coll.enabled = true;
        render.material = iceMaterial;
    }

    public void MeltIce()
    {
        coll.enabled = false;
        render.material = waterMaterial;
    }

    private void DisableScriptIfMissingComponent(string missingRefName)
    {
        Debug.Log($"{name}: Missing reference to {missingRefName}. Component will be disabled.");
        enabled = false;
    }
}
