using UnityEngine;

public class SummonView : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private string summonAnimationName;
    [SerializeField] private string dismissAnimationName;

    private bool isSummoned = false;
    private bool isDismissed = false;

    private void Awake()
    {
        if (!animator)
            DisableScriptIfMissingComponent("Animator ('Animator')");

        if (summonAnimationName == null || summonAnimationName == "")
            DisableScriptIfMissingComponent("String ('Summon Animation Name')");

        if (dismissAnimationName == null || dismissAnimationName == "")
            DisableScriptIfMissingComponent("String ('Summon Animation Name')");
    }

    private void OnDisable()
    {
        // Reset anim status
        isSummoned = false;
        isDismissed = false;
    }

    private void Update()
    {
        // animator.SetBool(summonAnimationName, isSummoned); // TODO: Uncomment
        // animator.SetBool(dismissAnimationName, isDismissed);
    }

    public bool IsAnimationBeingPlayed()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }

    public void PlaySummonAnimation()
    {
        isSummoned = true;
        isDismissed = false;
    }

    public void PlayDismissAnimation()
    {
        isDismissed = true;
        isSummoned = false;
    }

    // TODO: Move to Utils script
    private void DisableScriptIfMissingComponent(string missingRefName)
    {
        Debug.Log($"{name}: Missing reference to {missingRefName}. Component will be disabled.");
        enabled = false;
    }
}