using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float moveSpeed = 0f;

    [Space(10)]
    [SerializeField] private float jumpHeight = 1.2f;
    [SerializeField] private float gravity = -15.0f;
    [SerializeField] private float terminalVelocity = 53.0f;

    [Header("Player Grounded")]
    [SerializeField] private float groundedOffset = 0.85f;
    [SerializeField] private float groundedRadius = 0.5f;
    [SerializeField] private LayerMask groundLayers;

    [Header("Player Slide")]
    [SerializeField] private float maxSlideSpeed = 4.0f;
    [SerializeField] private float acceleration = 10.0f;
    [SerializeField] private float deceleration = 10.0f;
    [SerializeField] private LayerMask iceLayer;

    private float _verticalVelocity;
    public float _currentSlideSpeed = 0.0f;
    private float previousDirection = 0f;

    private CharacterController _controller;

    private bool grounded = true;
    private bool onIce;

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
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GroundedCheck();
        JumpAndGravity();

        if(!onIce)
        {
            Move();
        }

        else
        {
            Slide();
        }
    }

    private void GroundedCheck()
    {
        Vector3 spherePosition = new(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
        onIce = Physics.CheckSphere(spherePosition, groundedRadius, iceLayer, QueryTriggerInteraction.Ignore);
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
        Vector3 moveValue = new (direction.x, 0f, 0f);

        moveValue = moveValue.x * transform.right;

        _controller.Move(moveValue.normalized * (moveSpeed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }

    private void Slide()
    {
        Vector3 moveDirection = new Vector3(previousDirection, 0f, 0f).normalized;
        Vector3 moveValue = moveDirection * _currentSlideSpeed;
        _controller.Move(moveValue * Time.deltaTime + Vector3.up * _verticalVelocity * Time.deltaTime);

        UpdateSlideSpeed();
    }

    private void UpdateSlideSpeed()
    {
        float currentDirection = direction.x;

        if (currentDirection != 0f)
        {
            if (currentDirection != previousDirection)
            {
                _currentSlideSpeed = Mathf.MoveTowards(_currentSlideSpeed, 0f, (deceleration * 2) * Time.deltaTime);
                if (_currentSlideSpeed <= 0) previousDirection = currentDirection;
            }
            else
            {
                _currentSlideSpeed = Mathf.MoveTowards(_currentSlideSpeed, maxSlideSpeed, acceleration * Time.deltaTime);
            }
        }

        else
        {
            _currentSlideSpeed = Mathf.MoveTowards(_currentSlideSpeed, 0f, deceleration * Time.deltaTime);
        }

    }
}
