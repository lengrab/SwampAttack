using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace PlayerInput
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private float _minDeviation = 0.1f;
        [SerializeField] private UnityEvent<InputAction.CallbackContext> _move;
        [SerializeField] private UnityEvent<InputAction.CallbackContext> _jump;
        [SerializeField] private UnityEvent<InputAction.CallbackContext> _soot;
        [SerializeField] private UnityEvent<InputAction.CallbackContext> _action;

        private Input _input;
        private IEnumerator _readCorutine;
        private WaitForFixedUpdate _waitForFixed = new WaitForFixedUpdate();

        private void Awake()
        {
            _input = new Input();
        }

        private void OnEnable()
        {
            _input.Enable();
            _input.Player.Move.performed += OnContinuesRead;
            _input.Player.Jump.started += _jump.Invoke;
            _input.Player.Attack.performed += _soot.Invoke;
            _input.Player.Action.started += _action.Invoke;
        }

        private void OnDisable()
        {
            _input.Disable();
            _input.Player.Move.performed -= OnContinuesRead;
            _input.Player.Jump.started -= _jump.Invoke;
            _input.Player.Attack.performed -= _soot.Invoke;
            _input.Player.Action.started -= _action.Invoke;
        }

        private void OnContinuesRead(InputAction.CallbackContext context)
        {
            _readCorutine = Read(context);
            if (_readCorutine != null)
            {
                StartCoroutine(_readCorutine);
            }
        }

        private IEnumerator Read(InputAction.CallbackContext context)
        {
            while (Mathf.Abs(context.ReadValue<Vector2>().x) > _minDeviation)
            {
                _move.Invoke(context);
                yield return _waitForFixed;
            }
            
            _readCorutine = null;
        }
    }
}