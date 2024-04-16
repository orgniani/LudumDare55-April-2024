using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    [SerializeField] private GameObject summoningMenu;
    [SerializeField] private List<SummoningSystem> patternList = new List<SummoningSystem>();

    [Header("Settings")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private bool playerHasSpiritStones = false;

    private Dictionary<SummonType, SummonZone> _activeZonesByType = new();
    private float _currentCooldown = 0f;

    private void OnEnable()
    {
        uiManager.SetPlayerHasSpiritStones(playerHasSpiritStones);

        foreach (SummoningSystem pattern in patternList)
        {
            pattern.onWon += TriggerSummon;
        }
    }

    private void OnDisable()
    {
        foreach (SummoningSystem pattern in patternList)
        {
            pattern.onWon -= TriggerSummon;
        }
    }

    private void Update()
    {
        if (_currentCooldown > 0f)
            _currentCooldown -= Time.deltaTime;
    }

    public void SetPlayerHasSpiritStones(bool playerHasSpiritStones)
    {
        this.playerHasSpiritStones = playerHasSpiritStones;
        uiManager.SetPlayerHasSpiritStones(playerHasSpiritStones);
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

    public void OpenSummoningMenu(SummonType type)
    {
        foreach (SummoningSystem pattern in patternList)
        {
            if(pattern.SummonType == type)
            {
                if (_activeZonesByType.TryGetValue(type, out SummonZone activeZone) && playerHasSpiritStones
                    && _currentCooldown <= 0f)
                {
                    summoningMenu.SetActive(true);
                    pattern.gameObject.SetActive(true);
                }
                else
                {
                    Debug.Log($"{name}: Can't trigger pattern menu. Wrong input.");
                }
            }

            else
            {
                pattern.gameObject.SetActive(false);
            }
        }
    }

    private void TriggerSummon(SummonType type)
    {
        StartCoroutine(WaitToCast(type));
    }

    private IEnumerator WaitToCast(SummonType type)
    {
        yield return new WaitForSeconds(1f);

        if (_activeZonesByType.TryGetValue(type, out SummonZone activeZone) && playerHasSpiritStones
            && _currentCooldown <= 0f)
        {
            _currentCooldown = activeZone.GetSummonTotalDuration();
            uiManager.TriggerIconCooldownOverlay(type, _currentCooldown);
            activeZone.Summon();

        }
        else
        {
            Debug.Log($"{name}: Can't trigger summon. _currentCooldown is {_currentCooldown}, playerHasSpiritStones is {playerHasSpiritStones}");
        }

        yield return new WaitForSeconds(1f);
        summoningMenu.SetActive(false);
    }
}