using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerManager playerManager;

    private void Awake()
    {
        if (!player)
        {
            DisableScriptIfMissingComponent("Player Controller ('Player')");
            return;
        }

        if (!playerManager)
        {
            DisableScriptIfMissingComponent("Player Manager ('Player Manager')");
            return;
        }
    }

    public void SetMoveValue(InputAction.CallbackContext inputContext)
    {
        player.Direction = inputContext.ReadValue<Vector2>();
    }

    public void SetJump(InputAction.CallbackContext inputContext)
    {
        if (inputContext.performed)
            player.Jump = true;
    }

    public void TriggerHandSummon(InputAction.CallbackContext inputContext)
    {
        if (inputContext.performed)
            playerManager.OpenSummoningMenu(SummonType.Hand);
    }

    public void TriggerIceSummon(InputAction.CallbackContext inputContext)
    {
        if (inputContext.performed)
            playerManager.OpenSummoningMenu(SummonType.Ice);
    }

    public void TriggerBarrierSummon(InputAction.CallbackContext inputContext)
    {
        if(inputContext.performed)
            playerManager.OpenSummoningMenu(SummonType.Barrier);
    }

    private void DisableScriptIfMissingComponent(string missingRefName)
    {
        Debug.Log($"{name}: Missing reference to {missingRefName}. Component will be disabled.");
        enabled = false;
    }
}