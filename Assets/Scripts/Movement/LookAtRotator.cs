using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtRotator : MonoBehaviour,IInputListener
{
    [SerializeField] private Transform _objectToRotate;

    public void LookAtDirection(Vector3 lookDirection)
    {
        _objectToRotate.LookAt(_objectToRotate.transform.position + lookDirection);
    }

    public void ApplyInputResult(Vector3 direction)
    {
        LookAtDirection(direction);
    }
}
