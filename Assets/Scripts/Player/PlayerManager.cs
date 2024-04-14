using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // TODO: Check with Ceci to merge logic!
    [Header("Settings")]
    [SerializeField] private float cooldownDuration = 10f;

    private Dictionary<SummonType, SummonZone> _activeZonesByType = new();
    private float _currentCooldown = 0f;

    private void Update()
    {
        if (_currentCooldown > 0f)
            _currentCooldown -= Time.deltaTime;
    }

    public void AddActiveSummonZone(SummonType type, SummonZone activeZone)
    {
        _activeZonesByType.Add(type, activeZone);
        Debug.Log($"{name}: Added zone: {type}, current active zones are {_activeZonesByType.Count}");
    }

    public void RemoveInactiveSummonZone(SummonType type)
    {
        _activeZonesByType.Remove(type);
        Debug.Log($"{name}: Removed inactive zone {type}, current active zones are {_activeZonesByType.Count}");
    }

    public void TriggerSummon(SummonType type)
    {
        if (_activeZonesByType.TryGetValue(type, out SummonZone activeZone) && _currentCooldown <= 0f)
        {
            _currentCooldown = cooldownDuration;
            activeZone.Summon();
        } else
        {
            Debug.Log($"{name}: Can't trigger summon, current cooldown is {_currentCooldown}");
        }
    }
}