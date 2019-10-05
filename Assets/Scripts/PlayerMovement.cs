using System;
using UnityEngine;

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
    private float _maxVelocity = 2f;

    [SerializeField]
    [Range(0.005f, 1f)]
    private float _crouchMulti = 0.8f;

    [SerializeField]
    [Range(1f, 10f)]
    private float _dragForce = 1f;

    private PlayerInput _input;

    [SerializeField]
    private float _targetSpeed = 0f;
    private float _moveSpeed = 0f;

    private Rigidbody _rb;

    private Animator _anim;

    [SerializeField]
    [Range(1f, 50f)]
    private float _jumpForce = 5f;

    private int _currentJumps = 0;

    private bool _isCrouching = false;

    void Awake()
    {
        _abilities = GetComponent<PlayerAbilities>();
        _input = new PlayerInput();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();

        _input.Player.LeftRight.performed += ctx => {
            float v = ctx.ReadValue<float>();

            if (v > 0 && _abilities.Has(Ability.MoveRight) == false) return;
            if (v < 0 && _abilities.Has(Ability.MoveLeft) == false) return;

            _targetSpeed = v * _moveMulti;
        };

        _input.Player.LeftRight.canceled += ctx => {
            _targetSpeed = 0f;
        };

        _input.Player.Jump.performed += ctx => {
            Jump();
        };

        _input.Player.Crouch.performed += ctx => {
            Crouch();
        };

        _input.Player.Crouch.canceled += ctx => {
            Uncrouch();
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

    void Update() {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    void FixedUpdate()
    {
        float speed = _isCrouching
            ? _targetSpeed * _crouchMulti
            : _targetSpeed;

        _rb.AddForce(new Vector3(speed, 0f, 0f), ForceMode.Impulse);

        float xVelocity = _rb.velocity.x;
        float absoluteVelocity = Mathf.Abs(xVelocity);

        if (absoluteVelocity > _maxVelocity) {
            Vector3 force = new Vector3(xVelocity * -1f * _dragForce, 0f, 0f);
            _rb.AddForce(force, ForceMode.Impulse);
        }
    }

    void Jump()
    {
        int jumpsAvailable = _abilities.Has(Ability.DoubleJump)
            ? 2
            : _abilities.Has(Ability.Jump)
            ? 1
            : 0;

        if (_currentJumps >= jumpsAvailable) return;

        _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
        _rb.AddForce(new Vector3(0f, _jumpForce * 9.81f, 0f), ForceMode.Impulse);
        _currentJumps += 1;
    }

    void Crouch()
    {
        if (_abilities.Has(Ability.Crouch)) {
            _isCrouching = true;
            _anim.SetBool("IsCrouch", true);
        }
    }

    void Uncrouch()
    {
        if (_abilities.Has(Ability.Crouch)) {
            _isCrouching = false;
            _anim.SetBool("IsCrouch", false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Floor")) {
            _currentJumps = 0;
        }
    }
}
