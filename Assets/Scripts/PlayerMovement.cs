using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
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

    void Awake ()
    {
        _input = new PlayerInput();
        _rb = GetComponent<Rigidbody>();

        _input.Player.LeftRight.performed += ctx => {
            _targetSpeed = ctx.ReadValue<float>() * _moveMulti;
        };

        _input.Player.LeftRight.canceled += ctx => {
            _targetSpeed = 0f;
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
}
