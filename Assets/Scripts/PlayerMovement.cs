﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerAbilities))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerAbilities _abilities;

    [SerializeField]
    [Range(1f, 10f)]
    private float _moveMulti = 2f;

    [SerializeField]
    [Range(1f, 10f)]
    private float _accelMulti = 2f;

    private PlayerInput _input;

    [SerializeField]
    private float _targetSpeed = 0f;
    private float _moveSpeed = 0f;

    private Rigidbody _rb;

    [SerializeField]
    [Range(1f, 50f)]
    private float _jumpForce = 5f;

    void Awake()
    {
        _abilities = GetComponent<PlayerAbilities>();
        _input = new PlayerInput();
        _rb = GetComponent<Rigidbody>();

        _input.Player.LeftRight.performed += ctx => {
            _targetSpeed = ctx.ReadValue<float>() * _moveMulti;
        };

        _input.Player.LeftRight.canceled += ctx => {
            _targetSpeed = 0f;
        };

        _input.Player.Jump.performed += ctx => {
            Jump();
        };
    }

    void OnEnable()
    {
        _input.Player.Enable();
    }

    void OnDisable()
    {
        _input.Player.Disable();
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        Vector3 pos = new Vector3(transform.position.x + _targetSpeed, transform.position.y, 0f);
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * _accelMulti);
    }

    void Jump()
    {
        if (_abilities.Has(Ability.Jump) == false) return;

        _rb.AddForce(new Vector3(0f, _jumpForce * 9.81f, 0f), ForceMode.Impulse);
    }
}