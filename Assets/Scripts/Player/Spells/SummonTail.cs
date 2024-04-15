using System.Collections;
using UnityEngine;

public class SummonTail : SummonSpell
{
    [SerializeField] private FreezeEffect freezeZoneEffect;

    public override void TriggerSummon()
    {
        gameObject.SetActive(true);
        Debug.Log($"{name}: SummonSpell enabled.");
        StartCoroutine(SummonCoroutine());
    }

    private IEnumerator SummonCoroutine()
    {
        if (summonView != null)
        {
            // Summon
            summonView.PlaySummonAnimation();
            yield return new WaitForSeconds(animationDuration);
            Debug.Log($"{name}: Summon was completed.");

            // Freeze
            freezeZoneEffect.FreezeWater();

            // Dismiss
            //summonView.PlayDismissAnimation();
            yield return new WaitForSeconds(animationDuration);
            // Debug.Log($"{name}: Dismiss completed.");
        }

        // Wait summonDuration
        yield return new WaitForSeconds(summonAvailableDuration);
        Debug.Log($"{name}: Summon duration completed.");

        // Melt
        freezeZoneEffect.MeltIce();

        ReenableZoneForNextSummon();
        Debug.Log($"{name}: Zone re-enabled.");

        gameObject.SetActive(false);
        Debug.Log($"{name}: SummonSpell disabled.");
    }
}