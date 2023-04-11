using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitialization : MonoBehaviour
{
    [SerializeField] private InputObserver _inputObserver;
    [SerializeField] private List<MonoBehaviour> _listeners;

    private void Awake()
    {
        var inputListeners = new List<IInputListener>();
        foreach (var listener in _listeners)
        {
            if (listener is IInputListener inputListener)
            {
                inputListeners.Add(inputListener);
            }
        }
        _inputObserver.Init(inputListeners);
    }
}
