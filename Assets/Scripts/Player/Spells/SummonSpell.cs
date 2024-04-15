using System.Collections;
using UnityEngine;

/* 
 * This class handles all summoning/dismissing logic, whatever effect 
 * the summon should do (create platform, freeze water, put up barrier, etc)
 * as well as playing the corresponding audio and animations.
 */
public class SummonSpell : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [Header("View")]
    [SerializeField] private SummonView summonView;
    [SerializeField] private float animationDuration = 2.5f;
    [Header("Settings")]
    [SerializeField] private SummonType summonType = SummonType.Unassigned;
    [SerializeField] private Vector3 summonPosition = Vector3.zero;
    [SerializeField] private float summonDuration = 5f;

    private SummonZone _summonZone;

    private void Awake()
    {
        if (!audioSource)
        {
            DisableScriptIfMissingComponent("Audio Source ('Audio Source')");
            return;
        }

        if (SummonType.Unassigned.Equals(summonType))
        {
            DisableScriptIfMissingComponent("Summon Type ('Summon Type')");
            return;
        }

        if (!summonView)
        {
            DisableScriptIfMissingComponent("Summon View ('Summon View')");
            return;
        }

        transform.position = summonPosition;
    }

    private void OnEnable()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    private void OnDisable()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    public void TriggerSummon()
    {
        gameObject.SetActive(true);
        Debug.Log($"{name}: SummonSpell enabled.");
        StartCoroutine(SummonCoroutine());
    }

    private IEnumerator SummonCoroutine()
    {
        // Summon
        if (summonView != null)
        {
            summonView.PlaySummonAnimation();
            yield return new WaitForSeconds(animationDuration);
            Debug.Log($"{name}: Summon was completed.");
        }

        // Wait summonDuration
        yield return new WaitForSeconds(summonDuration);
        Debug.Log($"{name}: Summon duration completed.");

        // Dismiss
        if (summonView != null)
        {
            summonView.PlayDismissAnimation();
            yield return new WaitForSeconds(animationDuration);
            Debug.Log($"{name}: Dismiss completed.");
        }

        ReenableZoneForNextSummon();

        gameObject.SetActive(false);
        Debug.Log($"{name}: SummonSpell disabled.");
    }

    public SummonType GetSummonType()
    {
        return summonType;
    }

    public void SetSummonZoneReference(SummonZone summonZone)
    {
        _summonZone = summonZone;
    }

    private void ReenableZoneForNextSummon()
    {
        if (_summonZone != null)
        {
            _summonZone.ReenableZoneForNextSummon();
            _summonZone = null;
        }
    }

    private void DisableScriptIfMissingComponent(string missingRefName)
    {
        Debug.Log($"{name}: Missing reference to {missingRefName}. Component will be disabled.");
        enabled = false;
    }
}