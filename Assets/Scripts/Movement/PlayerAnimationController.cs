using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour, IInputListener
{
    [SerializeField] private Animator _animator;
    private int _moveKey = Animator.StringToHash("is-moving");
    public void ApplyInputResult(Vector3 direction)
    {
        var isMoving = direction.magnitude > 0;
        _animator.SetBool(_moveKey, isMoving);
    }
}
