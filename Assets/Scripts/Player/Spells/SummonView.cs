using UnityEngine;

public class SummonView : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private string summonAnimationName;
    [SerializeField] private string dismissAnimationName;

    private void Awake()
    {
        if (!animator)
            DisableScriptIfMissingComponent("Animator ('Animator')");

        if (summonAnimationName == null || summonAnimationName == "")
            DisableScriptIfMissingComponent("String ('Summon Animation Name')");

        if (dismissAnimationName == null || dismissAnimationName == "")
            DisableScriptIfMissingComponent("String ('Summon Animation Name')");
    }

    public bool IsAnimationBeingPlayed()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }

    public void PlaySummonAnimation()
    {
        animator.Play(summonAnimationName);
    }

    public void PlayDismissAnimation()
    {
        animator.Play(dismissAnimationName);
    }

    private void DisableScriptIfMissingComponent(string missingRefName)
    {
        Debug.Log($"{name}: Missing reference to {missingRefName}. Component will be disabled.");
        enabled = false;
    }
}