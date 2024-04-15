using UnityEngine;

/*
 * This class handles all SummonZone logic, casting summoning spells on input received 
 * and sending info to PlayerManager so summons can be cast.
 */
public class SummonZone : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerManager playerManager;
    [Header("Spell")]
    [SerializeField] private SummonSpell spell;
    [Header("Settings")]
    [SerializeField] private BoxCollider zoneCollider;
    [SerializeField] private float totalSummonCooldown = 10f;

    private bool _canBeSummoned;
    private SummonType _summonType = SummonType.Unassigned;

    private void Awake()
    {
        if (!playerManager)
        {
            DisableScriptIfMissingComponent("Player Manager ('Player Manager')");
            return;
        }

        if (!spell)
        {
            DisableScriptIfMissingComponent("Summon Spell ('Spell')");
            return;
        }

        if (!zoneCollider)
        {
            DisableScriptIfMissingComponent("Box Collider ('Zone Collider')");
            return;
        }
    }

    private void OnEnable()
    {
        _summonType = spell.GetSummonType();
    }

    public void ReenableZoneForNextSummon()
    {
        _canBeSummoned = true;
    }

    public float GetSummonTotalDuration()
    {
        return totalSummonCooldown;
    }

    public void Summon()
    {
        if (_canBeSummoned)
        {
            Debug.Log($"{name}: {_summonType} summon triggered.");
            spell.TriggerSummon();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // TODO: Change to layer?
        if(other.name == "Player")
        {
            Debug.Log($"{name}: Player entered trigger, summon enabled.");
            EnableSummon();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // TODO: Change to layer?
        if (other.name == "Player")
        {
            Debug.Log($"{name}: Player exited trigger, summon disabled.");
            DisableSummon();
        }
    }

    private void DisableScriptIfMissingComponent(string missingRefName)
    {
        Debug.Log($"{name}: Missing reference to {missingRefName}. Component will be disabled.");
        enabled = false;
    }

    private void EnableSummon()
    {
        _canBeSummoned = true;
        playerManager.AddActiveSummonZone(_summonType, this);
    }

    private void DisableSummon()
    {
        _canBeSummoned = false;
        playerManager.RemoveInactiveSummonZone(_summonType);
    }
}