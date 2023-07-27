using UnityEngine;

public class GroundedChecker : MonoBehaviour, IGroundedChecker
{
    [SerializeField]
    private float groundCheckDistance = 0.1f;

    [SerializeField]
    private string _groundLayerName = "Ground";

    private LayerMask _groundLayer;
    private Collider2D _capsuleCollider;
    private bool _isGrounded;

    private void Awake()
    {
        _capsuleCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        _groundLayer = LayerMask.GetMask(_groundLayerName);
    }

    private void FixedUpdate()
    {
        GroundedCheck();
    }

    public bool GetIsGrounded()
    {
        return _isGrounded;
    }

    private void GroundedCheck()
    {
        var x = _capsuleCollider.bounds.center.x;
        var y = _capsuleCollider.bounds.min.y;
        var raycastOrigin = new Vector2(x, y);
        var hit = Physics2D.Raycast(raycastOrigin, Vector2.down, groundCheckDistance, _groundLayer);
        _isGrounded = hit.collider != null;
    }
}