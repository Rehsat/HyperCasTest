using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Market.PlayerControl
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyMovement : MonoBehaviour, IInputListener
    {
        [SerializeField] private float _speed;
        private Rigidbody _rigidbody;
        private Vector3 _direction;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _direction * _speed;
        }

        public void ApplyInputResult(Vector3 direction)
        {
            SetDirection(direction);
        }
    }
}

