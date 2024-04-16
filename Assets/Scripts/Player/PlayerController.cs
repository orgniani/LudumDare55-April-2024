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

    private CharacterController controller;
    private Animator characterAnimator;

    private bool grounded = true;
    private bool onIce;

    public bool jump;
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
        characterAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        GroundedCheck();
        JumpAndGravity();
        if (!onIce)
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
        Vector3 spherePosition = new(transform.position.x, transform.position.y - groundedOffset, 0f);
        grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
        onIce = Physics.CheckSphere(spherePosition, groundedRadius, iceLayer, QueryTriggerInteraction.Ignore);
    }

    public void JumpAndGravity()
    {
        if ((grounded || onIce) && jump)
        {
            characterAnimator.SetBool("isJumping", true);
        }
        else
            jump = false;

        if (_verticalVelocity < terminalVelocity)
        {
            _verticalVelocity += gravity * Time.deltaTime;
        }
    }

    private void Move()
    {
        Vector3 moveValue = new Vector3(direction.x, 0, 0);

        if (moveValue.x > 0)
        {
            characterAnimator.SetBool("isIdle", false);
            characterAnimator.SetBool("isWalkingRight", false);
            characterAnimator.SetBool("isWalkingLeft", true);
        }
        if (moveValue.x < 0)
        {;
            characterAnimator.SetBool("isIdle", false);
            characterAnimator.SetBool("isWalkingLeft", false);
            characterAnimator.SetBool("isWalkingRight", true);
        }
        if (moveValue.x == 0)
        {
            characterAnimator.SetBool("isWalkingRight", false);
            characterAnimator.SetBool("isWalkingLeft", false);
            characterAnimator.SetBool("isIdle", true);
        }

        moveValue = moveValue.x * transform.forward;
        controller.Move(moveValue.normalized * (moveSpeed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

    }

    private void Slide()
    {
        Vector3 moveDirection = new Vector3(previousDirection, 0f, 0f).normalized;
        Vector3 moveValue = moveDirection * _currentSlideSpeed;
        controller.Move(moveValue * Time.deltaTime + Vector3.up * _verticalVelocity * Time.deltaTime);

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

    public void Fall()
    {
        characterAnimator.SetBool("isJumping", false);
        characterAnimator.SetBool("isFalling", true);
    }

    public void Land()
    {
        GroundedCheck();
        if (grounded || onIce)
        {
            characterAnimator.SetBool("isJumping", false);
            characterAnimator.SetBool("isFalling", false);
            characterAnimator.SetBool("Landed", true);
        }
    }

    public void TakeOff()
    {
        _verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    public void BackToIdle()
    {
        jump = false;
        characterAnimator.SetBool("isJumping", false);
        characterAnimator.SetBool("Landed", false);
    }
}
