using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(IGroundedChecker))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    [Min(0)]
    private float _movementSpeed;

    [SerializeField]
    [Min(0)]
    private float _maxSpeed;

    [SerializeField]
    [Min(0)]
    private float _groundSpeedMultiplier;

    [SerializeField]
    [Min(0)]
    private float _jumpSpeed;

    private Vector2 _jumpDirection = Vector2.up;
    private bool IsGrounded => _groundedChecker.GetIsGrounded();
    private Vector2 _moveInputVector;

    private PlayerInput _playerInput;
    private Rigidbody2D _rigidbody2D;
    private IGroundedChecker _groundedChecker;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _groundedChecker = GetComponent<IGroundedChecker>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable() => _playerInput.enabled = true;

    private void OnDisable() => _playerInput.enabled = false;

    private void FixedUpdate() => Move();

    public void Move()
    {
        if (!(_rigidbody2D.velocity.magnitude < _maxSpeed))
            return;

        var velocity = _moveInputVector * _movementSpeed;
        if (IsGrounded)
            velocity *= _groundSpeedMultiplier;

        _rigidbody2D.AddForce(velocity * _rigidbody2D.mass);
    }

    public void OnMove(InputValue value)
    {
        _moveInputVector = value.Get<Vector2>();
    }

    public void OnJump()
    {
        if (!IsGrounded)
            return;

        _rigidbody2D.AddForce(_jumpDirection * _jumpSpeed * _rigidbody2D.mass, ForceMode2D.Impulse);
    }
}