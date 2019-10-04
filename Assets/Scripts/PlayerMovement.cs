using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 10f)]
    private float _moveSpeedMultiplier = 2f;

    private PlayerInput _input;

    [SerializeField]
    private float _moveSpeed = 0f;

    private Rigidbody _rb;

    void Awake ()
    {
        _input = new PlayerInput();
        _rb = GetComponent<Rigidbody>();

        _input.Player.LeftRight.performed += ctx => {
            _moveSpeed = ctx.ReadValue<float>();
        };

        _input.Player.LeftRight.canceled += ctx => {
            _moveSpeed = 0f;
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
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        Vector3 force = new Vector3(_moveSpeed * _moveSpeedMultiplier, 0f, 0f);
        _rb.AddForce(force, ForceMode.Impulse);
    }
}
