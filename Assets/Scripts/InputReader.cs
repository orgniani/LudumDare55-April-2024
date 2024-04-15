using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private void Awake()
    {
        if (!player)
        {
            Debug.LogError($"{name}: PlayerController is null.\nDisabling component.");
            enabled = false;
            return;
        }
    }

    public void SetMoveValue(InputAction.CallbackContext inputContext)
    {
        player.Direction = inputContext.ReadValue<Vector2>();
    }

    public void SetJump(InputAction.CallbackContext inputContext)
    {
        player.jump = true;
    }
}
