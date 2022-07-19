using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMover : MonoBehaviour
{
    private static readonly string EnemyHash = "Enemy";
    private static readonly float JumpNormalTreshold = 0.1f;

    [Range(0,10f)][SerializeField] private float _speed = 3;
    [SerializeField] private float _jumpHeight = 3;
    [SerializeField] private float _heigtCheck = 2;

    private Rigidbody2D _rigidbody;

    public bool IsGrounded => IsOnGround();

    public void Move(Vector2 direction)
    {
        direction = direction.normalized;
        float targetSpeed = direction.x * _speed;
        float deltaSpeed = targetSpeed - _rigidbody.velocity.x;
        Vector2 impulse = _rigidbody.mass * new Vector2(deltaSpeed, 0);
        _rigidbody.AddForce(impulse * Time.deltaTime, ForceMode2D.Impulse);
    }

    public void Jump()
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
        LayerMask mask = LayerMask.GetMask(EnemyHash);
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
