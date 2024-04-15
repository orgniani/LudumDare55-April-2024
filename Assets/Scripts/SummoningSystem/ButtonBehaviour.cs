using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    [SerializeField] private Image circleBorder;
    [SerializeField] private float animationSpeed = 1f;

    [SerializeField] private float minAlpha = 0f;

    private Color startColor;
    private Color endColor = new Color(1f, 1f, 1f, 1f);

    private Coroutine currentAnimation;

    private void Awake()
    {
        startColor = circleBorder.color;
        endColor = new Color(startColor.r, startColor.g, startColor.b, minAlpha);
        circleBorder.color = endColor;
    }

    public void OnPointerEnter()
    {
        if (currentAnimation != null)
            StopCoroutine(currentAnimation);

        currentAnimation = StartCoroutine(AnimateBorder(true));
    }

    public void OnPointerExit()
    {
        if (currentAnimation != null)
            StopCoroutine(currentAnimation);

        currentAnimation = StartCoroutine(AnimateBorder(false));
    }

    private IEnumerator AnimateBorder(bool fadeIn)
    {
        float t = 0f;
        Color start = fadeIn ? endColor : startColor;
        Color end = fadeIn ? startColor : endColor;

        while (t < 1f)
        {
            t += Time.deltaTime * animationSpeed;
            Color currentColor = Color.Lerp(start, end, t);
            circleBorder.color = currentColor;
            yield return null;
        }

        currentAnimation = null;
    }

}
