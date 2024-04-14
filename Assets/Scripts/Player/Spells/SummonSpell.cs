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
    [SerializeField] protected SummonView summonView;
    [Header("Settings")]
    [SerializeField] private SummonType summonType = SummonType.Unassigned;
    [SerializeField] protected float summonEffectSpeed = 10f;

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

    public virtual void Summon()
    {
        Debug.Log($"{name}: Summon() method not implemented.");
    }

    public virtual void Dismiss()
    {
        Debug.Log($"{name}: Dismiss() method not implemented.");
    }

    public SummonType GetSummonType()
    {
        return summonType;
    }

    // TODO: Move to Utils script?
    protected void DisableScriptIfMissingComponent(string missingRefName)
    {
        Debug.Log($"{name}: Missing reference to {missingRefName}. Component will be disabled.");
        enabled = false;
    }
}