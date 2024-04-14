using System.Collections;
using UnityEngine;

public class Barrier : SummonSpell
{
    [Header("Barrier Settings")]
    [SerializeField] private Vector3 barrierSummonedPosition = Vector3.zero;
    [SerializeField] private Vector3 barrierDismissedPosition = new(0f, -1f, 0f); 

    private bool _triggerSummon = false;
    private bool _triggerDismiss = false;

    private void Update()
    {
        if (_triggerSummon)
        {
            SummonWall();
        }
        else if (_triggerDismiss)
        {
            DismissWall();
        }
    }

    public override void Summon()
    {
        Debug.Log($"{name}: Commencing barrier summoning...");
        enabled = true;
        _triggerSummon = true;

        if (summonView != null)
        {
            /* TODO: 
             * We could wait here for animation to be finished
             * but since SummonZone::SummoningCoroutine has a delay...
             */
            // summonView.PlaySummonAnimation(); // TODO: Uncomment
            Debug.Log($"{name}: Play summon barrier animation!"); // TODO: Remove
        }
    }

    public override void Dismiss()
    {
        StartCoroutine(DismissSummonCoroutine());
    }

    public void DestroyOnImpact()
    {
        // TODO if hit by danger/enemy then destroy? !!! -> Requires new inheritance overriding SummonZone::Summon
    }

    private void SummonWall()
    {
        if (transform.position.y < barrierSummonedPosition.y)
        {
            float yPosition = transform.position.y + Time.deltaTime * summonEffectSpeed;
            transform.position = new(transform.position.x, yPosition, transform.position.z);
        } else
        {
            Debug.Log($"{name}: Finished barrier summoning.");
            _triggerSummon = false;
        }
    }

    private void DismissWall()
    {
        if (transform.position.y > barrierDismissedPosition.y)
        {
            float yPosition = transform.position.y - Time.deltaTime * summonEffectSpeed;
            transform.position = new(transform.position.x, yPosition, transform.position.z);
        }
        else
        {
            Debug.Log($"{name}: Finished barrier dismissing.");
            _triggerDismiss = false;
        }
    }

    private IEnumerator DismissSummonCoroutine()
    {
        _triggerDismiss = true;
        // summonView.PlayDismissAnimation(); // TODO: Uncomment
        // yield return new WaitUntil(() => summonView.IsAnimationBeingPlayed()); // TODO: Uncomment
        yield return new WaitForSeconds(5f); // TODO: Remove
        enabled = false;
        Debug.Log($"{name}: Barrier was disabled.");
    }
}