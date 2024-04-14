using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LineFeedback : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 targetPosition;
    [SerializeField] private float animationSpeed = 1f;

    private Material lineMaterial;
    private Color startColor;
    private Color endColor = new Color(1f, 1f, 1f, 1f); // Full alpha

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineMaterial = lineRenderer.material;
        startColor = lineMaterial.color;
        startColor.a = 0f; // Start with fully transparent
        lineMaterial.color = startColor;
    }

    public void ShowShotDirection(Vector3 endPosition)
    {
        targetPosition = endPosition;
        StartCoroutine(AnimateLine());
    }

    private IEnumerator AnimateLine()
    {
        lineRenderer.enabled = true;
        float t = 0f;
        Vector3 startPosition = transform.position;
        float distance = Vector3.Distance(startPosition, targetPosition);

        while (t < 1f)
        {
            t += Time.deltaTime * animationSpeed;
            Vector3 newPosition = transform.InverseTransformPoint(startPosition + Vector3.ClampMagnitude(targetPosition - startPosition, distance * t));
            lineRenderer.SetPosition(1, newPosition);

            // Interpolate alpha value
            Color currentColor = Color.Lerp(startColor, endColor, t);
            lineMaterial.color = currentColor;

            yield return null;
        }
    }
}
