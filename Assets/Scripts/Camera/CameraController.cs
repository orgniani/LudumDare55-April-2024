using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Settings")]
    [SerializeField] private Vector3 cameraOffset = new(0f, 1f, -10f);
    [SerializeField] private float smoothTime = 0.25f;
    [SerializeField] private Vector2 minBound = new(-6f, 0f);
    [SerializeField] private Vector2 maxBound = new(6f, 10f);

    private Vector3 _velocity = Vector3.zero;

    private void Awake()
    {
        if (!target)
        {
            Debug.LogError($"{name}: Select a Target.\nDisabling component.");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        MoveCamera();
    }

    /* 
     * Camera will follow the player within pre-defined bounds.
     * If it reaches a bound it'll stay still until the character moves away from the bound.
     */
    private void MoveCamera()
    {
        Vector3 cameraPosition = target.position + cameraOffset;
        cameraPosition = new Vector3(
            Mathf.Clamp(cameraPosition.x, minBound.x, maxBound.x),
            Mathf.Clamp(cameraPosition.y, minBound.y, maxBound.y),
            cameraPosition.z
        );

        // Camera moves to the calculated position with a delay of smoothTime seconds.
        transform.position = Vector3.SmoothDamp(transform.position, cameraPosition, ref _velocity, smoothTime);
    }
}
