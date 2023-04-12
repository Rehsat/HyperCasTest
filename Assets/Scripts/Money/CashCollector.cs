using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CashCollector : MonoBehaviour
{
    
    [SerializeField] private TriggerEnterObserver _observer;
    private CashContainer _container = new CashContainer();
    
    
    public Transform Transform => _observer.transform;

   
    public void AddCash()
    {
        _container.CashCount++;
    }
}
