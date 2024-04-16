using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SummonIcon : MonoBehaviour
{
    [Header("Image")]
    [SerializeField] private Image summonIcon;
    [SerializeField] private Image cooldownOverlay;

    [Header("Settings")]
    [SerializeField] private SummonType summonType = SummonType.Unassigned;

    private float _currentCooldownTime = 0f;
    private bool _isZoneActive = false;

    private void Awake()
    {
        if (!summonIcon)
        {
            DisableScriptIfMissingComponent("Image ('Summon Icon')");
            return;
        }

        if (!cooldownOverlay)
        {
            DisableScriptIfMissingComponent("Image ('Cooldown Overlay')");
            return;
        }

        if (SummonType.Unassigned.Equals(summonType))
        {
            DisableScriptIfMissingComponent("Summon Type ('Summon Type')");
            return;
        }
    }

    private void OnEnable()
    {
        cooldownOverlay.fillAmount = 1;
        _currentCooldownTime = 0f;
    }

    public void ShowCooldown(float cooldownTime)
    {
        Debug.Log($"{name}: ShowCooldown was triggered, cooldownTime is {cooldownTime}.");
        cooldownOverlay.gameObject.SetActive(true);
        StartCoroutine(FillImageWithTime(cooldownTime));
    }

    private IEnumerator FillImageWithTime(float cooldownTime)
    {
        while (_currentCooldownTime < cooldownTime)
        {
            _currentCooldownTime += Time.deltaTime;
            cooldownOverlay.fillAmount = (1 - _currentCooldownTime / cooldownTime);
            yield return null;
        }

        cooldownOverlay.gameObject.SetActive(false);
        _currentCooldownTime = 0f;
        Debug.Log($"{name}: cooldownOverlay is disabled!");
        yield break;
    }

    public SummonType GetSummonType()
    {
        return summonType;
    }
    
    public void SetIsZoneActive(bool isZoneActive)
    {
        _isZoneActive = isZoneActive;
        summonIcon.color = _isZoneActive ? Color.white : Color.gray;
    }

    private void DisableScriptIfMissingComponent(string missingRefName)
    {
        Debug.Log($"{name}: Missing reference to {missingRefName}. Component will be disabled.");
        enabled = false;
    }
}
