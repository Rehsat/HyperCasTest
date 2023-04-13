using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorablesCollector : MonoBehaviour
{
    [SerializeField] private TriggerEnterObserver _collectTrigger;
    [SerializeField] private Storage _storage;

    private TriggerExitObserver _collectExitObserver;

    private List<IStorablesContainer> _containers = new List<IStorablesContainer>();

    

    private void OnEnable()
    {
        _collectExitObserver = _collectTrigger.GetComponent<TriggerExitObserver>();
        _collectTrigger.OnEnterTrigger += TryPlaceStorables;
        _collectExitObserver.OnExitTrigger += TryRemoveStorables;
    }

    private void Update()
    {
        if (_containers.Count > 0 )
        {
            foreach (var container in _containers)
            {
                var emptySlotsCount = _storage.GetEmptySLotsCount();
                var storables = container.GetStorables(emptySlotsCount);
                foreach (var storable in storables)
                {
                    _storage.AddStorable(storable);
                }
            }
        }
    }

    private void TryPlaceStorables(Collider potentialStorable)
    {
        if (potentialStorable.TryGetComponent<IStorablesContainer>(out var storableContainer))
        {
            _containers.Add(storableContainer);
        }
    }
    private void TryRemoveStorables(Collider potentialStorable)
    {
        if (potentialStorable.TryGetComponent<IStorablesContainer>(out var storableContainer))
        {
            _containers.Remove(storableContainer);
        }
    }

    private void OnDisable()
    {
        _collectTrigger.OnEnterTrigger -= TryPlaceStorables;
        _collectExitObserver.OnExitTrigger -= TryRemoveStorables;
    }
}

public interface IStorablesContainer
{
    public List<Storable> GetStorables(int count);
}