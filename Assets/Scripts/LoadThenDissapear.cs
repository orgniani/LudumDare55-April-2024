using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadThenDissapear : MonoBehaviour
{
    [SerializeField] private float cooldownTime = 5.0f;

    private Image image;
    private float time = 0.0f;

    private void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }
    private void OnEnable()
    {
        image.fillAmount = 0;
        StartCoroutine(FillImageWithTime());
    }

    IEnumerator FillImageWithTime()
    {
        while (time < cooldownTime)
        {
            time += Time.deltaTime;
            image.fillAmount = time/cooldownTime;
            yield return null;
        }
        time = 0.0f;
        gameObject.SetActive(false);
        yield break;
    }

}
