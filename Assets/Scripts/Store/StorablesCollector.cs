using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorablesCollector : MonoBehaviour
{
    [SerializeField] private TriggerObserver _collectTrigger;
    [SerializeField] private Storage _storage;

    private void OnEnable()
    {
        _collectTrigger.OnEnterTrigger += TryPlaceStorables;
    }

    private void TryPlaceStorables(Collider potentialStorable)
    {
        if (potentialStorable.TryGetComponent<IStorablesContainer>(out var storableContainer))
        {
            var emptySlotsCount = _storage.GetEmptySLotsCount();
            var storables = storableContainer.GetStorables(emptySlotsCount);
            foreach (var storable in storables)
            {
                _storage.AddStorable(storable);
            }
        }
    }

    private void OnDisable()
    {
        _collectTrigger.OnEnterTrigger -= TryPlaceStorables;
    }
}

public interface IStorablesContainer
{
    public List<Storable> GetStorables(int count);
}