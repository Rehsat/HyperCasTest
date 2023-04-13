using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Shop
{
    [RequireComponent(typeof(NavMeshAgent), typeof(PlayerAnimationController))]
    public class ClientAnimationController : MonoBehaviour
    {
        [SerializeField] private float _velocityTolerance = 0.01f;
        private NavMeshAgent _agent;
        private PlayerAnimationController _animationController;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animationController = GetComponent<PlayerAnimationController>();
        }

        private void Update()
        {
            var velocity = _agent.velocity.magnitude > _velocityTolerance ? _agent.velocity : Vector3.zero;
            _animationController.ApplyInputResult(velocity);
        }
    }
}
