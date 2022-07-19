using System;
using UnityEngine;


    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyRenderer : MonoBehaviour
    {
        [SerializeField] private float _speedForUpdate = .15f;
        [SerializeField] private bool _flipOnStart = false;
        
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody2D;

        public void RotateToTarget(Transform target)
        {
            if (target.position.x > transform.position.x && _flipOnStart)
            {
                FlipX(!_flipOnStart);
            }
            else
            {
                FlipX(_flipOnStart);
            }
        }

        private void Update()
        {
            if (_rigidbody2D.velocity.magnitude > _speedForUpdate)
            {
                CheckFlip();
            }
        }

        private void CheckFlip()
        {
            if (_rigidbody2D.velocity.x > 0)
            {
                FlipX(_flipOnStart);
            }
            else
            {
                FlipX(!_flipOnStart);
            }
        }

        private void FlipX(bool isFlip)
        {
            if (isFlip == _spriteRenderer.flipX)
            {
                return;
            }
            
            _spriteRenderer.flipX = isFlip;
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }
