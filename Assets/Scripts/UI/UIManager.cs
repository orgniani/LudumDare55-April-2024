using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Spell Images")]
    [SerializeField] private SummonIcon hand;
    [SerializeField] private SummonIcon ice;
    [SerializeField] private SummonIcon barrier;

    [Header("Player")]
    [SerializeField] private PlayerManager playerManager;

    private Dictionary<SummonType, SummonIcon> _iconsByType = new();
    private bool _playerHasSpiritStones = false;

    private void Awake()
    {
        if (!hand)
        {
            DisableScriptIfMissingComponent("Image ('Hand Icon')");
            return;
        }

        if (!ice)
        {
            DisableScriptIfMissingComponent("Image ('Ice Icon')");
            return;
        }

        if (!barrier)
        {
            DisableScriptIfMissingComponent("Image ('Barrier Icon')");
            return;
        }

        if (!playerManager)
        {
            DisableScriptIfMissingComponent("Player Manager ('Player Manager')");
            return;
        }
    }

    private void OnEnable()
    {
        _iconsByType.Add(hand.GetSummonType(), hand);
        _iconsByType.Add(ice.GetSummonType(), ice);
        _iconsByType.Add(barrier.GetSummonType(), barrier);
    }

    public void TriggerIconCooldownOverlay(SummonType summonTypeInCooldown, float cooldownTime)
    {
        Debug.Log($"{name}: Trigger cooldown for type {summonTypeInCooldown} with cooldownTime {cooldownTime}");
        if(_iconsByType.TryGetValue(summonTypeInCooldown, out SummonIcon iconInCooldown) && _playerHasSpiritStones)
        {
            Debug.Log($"{name}: cooldown icon found in _iconsByType!");
            iconInCooldown.ShowCooldown(cooldownTime);
        }
    }

    public void SetIconIsZoneActive(SummonType summonType, bool isZoneActive)
    {
        if (_iconsByType.TryGetValue(summonType, out SummonIcon activeIcon))
        {
            activeIcon.SetIsZoneActive(isZoneActive);
        }
    }

    public void SetPlayerHasSpiritStones(bool playerHasSpiritStones)
    {
        _playerHasSpiritStones = playerHasSpiritStones;
    }

    private void DisableScriptIfMissingComponent(string missingRefName)
    {
        Debug.Log($"{name}: Missing reference to {missingRefName}. Component will be disabled.");
        enabled = false;
    }
}
