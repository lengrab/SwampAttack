using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    private static readonly string PlayerHash = "Player";
    private static readonly float JumpNormalTreshold = 0.1f;
    
    [SerializeField] private ForceMode2D _speedMode = ForceMode2D.Force;
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _jumpHeight = 3;
    [SerializeField] private float _heigtCheck = 2;
    
    private Rigidbody2D _rigidbody;

    public bool IsGrounded => IsOnGround();

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        _rigidbody.AddForce(_speed * new Vector2(inputVector.x,0) * Time.deltaTime, _speedMode);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (IsGrounded)
        {
            float mass = _rigidbody.mass;
            float gravityY = Mathf.Abs(Physics.gravity.y);
            float velosityY = Mathf.Sqrt(_jumpHeight * gravityY * 2);
            Vector2 jumpImpulse = Vector2.up * velosityY * mass;
            _rigidbody.AddForce(jumpImpulse, ForceMode2D.Impulse);
        }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private bool IsOnGround()
    {
        LayerMask mask = LayerMask.GetMask(PlayerHash);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _heigtCheck, ~mask);
        
        if (hit != null)
        {
            return (Vector2.Dot(hit.normal, Vector2.up) > JumpNormalTreshold);
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * _heigtCheck);
    }
}
