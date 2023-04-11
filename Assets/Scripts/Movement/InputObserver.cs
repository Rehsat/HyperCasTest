using System;
using System.Collections;
using System.Collections.Generic;
using Market.PlayerControl;
using UnityEngine;

public class InputObserver : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;

    private List<IInputListener> _listeners = new List<IInputListener>();

    public void Init(List<IInputListener> listeners)
    {
        _listeners = listeners;
    }
    private void OnEnable()
    {
        _joystick.OnDirectionChanged += MovePlayer;
    }

    private void MovePlayer(Vector2 inputDirection)
    {
        var direction = new Vector3(inputDirection.x, 0, inputDirection.y);
        foreach (var listener in  _listeners)
        {
            listener.ApplyInputResult(direction);
        }
    }

    private void OnDisable()
    {
        _joystick.OnDirectionChanged -= MovePlayer;
    }
}

public interface IInputListener
{
    public void ApplyInputResult(Vector3 direction);
}
