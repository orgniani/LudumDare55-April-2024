using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float moveSpeed = 4.0f;

    [Space(10)]
    [SerializeField] private float jumpHeight = 1.2f;
    [SerializeField] private float gravity = -15.0f;
    [SerializeField] private float terminalVelocity = 53.0f;

    [Header("Player Grounded")]
    [SerializeField] private bool grounded = true;
    [SerializeField] private float groundedOffset = 0.85f;
    [SerializeField] private float groundedRadius = 0.5f;
    [SerializeField] private LayerMask groundLayers;

    private CharacterController controller;

    private float _verticalVelocity;

    private bool jump;
    public bool Jump { set { jump = value; } }

    private Vector2 direction;
    public Vector2 Direction { set { direction = value; } }

    private void Awake()
    {

        if (groundLayers.value == 0)
        {
            Debug.LogError($"{name}: Select a LayerMask.\nDisabling component.");
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GroundedCheck();
        JumpAndGravity();
        Move();
    }

    private void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
    }

    private void JumpAndGravity()
    {
        if (grounded && jump)
            _verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        else
            jump = false;

        if (_verticalVelocity < terminalVelocity)
            _verticalVelocity += gravity * Time.deltaTime;
    }

    private void Move()
    {
        Vector3 moveValue = new Vector3(direction.x, 0, direction.y);

        moveValue = moveValue.x * transform.right + moveValue.z * transform.forward;

        controller.Move(moveValue.normalized * (moveSpeed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }
}
