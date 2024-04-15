using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    [Header("Settings")]
    [SerializeField] private bool playerHasSpiritStones = false;

    private Dictionary<SummonType, SummonZone> _activeZonesByType = new();
    private float _currentCooldown = 0f;

    private void OnEnable()
    {
        uiManager.SetPlayerHasSpiritStones(playerHasSpiritStones);
    }

    private void Update()
    {
        if (_currentCooldown > 0f)
            _currentCooldown -= Time.deltaTime;
    }

    public void AddActiveSummonZone(SummonType type, SummonZone activeZone)
    {
        _activeZonesByType.Add(type, activeZone);
        uiManager.SetIconIsZoneActive(type, true);
        Debug.Log($"{name}: Added zone: {type}, current active zones are {_activeZonesByType.Count}");
    }

    public void RemoveInactiveSummonZone(SummonType type)
    {
        _activeZonesByType.Remove(type);
        uiManager.SetIconIsZoneActive(type, false);
        Debug.Log($"{name}: Removed inactive zone {type}, current active zones are {_activeZonesByType.Count}");
    }

    public void TriggerSummon(SummonType type)
    {
        if (_activeZonesByType.TryGetValue(type, out SummonZone activeZone) && playerHasSpiritStones
            && _currentCooldown <= 0f)
        {
            _currentCooldown = activeZone.GetSummonTotalDuration();
            uiManager.TriggerIconCooldownOverlay(type, _currentCooldown);
            activeZone.Summon();
        } else
        {
            Debug.Log($"{name}: Can't trigger summon, current cooldown is {_currentCooldown}");
        }
    }
}