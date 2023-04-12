using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnterObserver : MonoBehaviour
{
    [SerializeField] private bool _debug;
    [SerializeField] private LayerMask _observableLayer;
    public Action<Collider> OnEnterTrigger { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (_debug)
            Debug.LogError(other.gameObject.name);
        if (other.gameObject.IsInLayer(_observableLayer))
        {
            if (_debug)
                Debug.LogError(other.gameObject.name + 5);
            OnEnterTrigger?.Invoke(other);
        }
    }
}
public static class GameObjectExtensions 
{
    public static bool IsInLayer(this GameObject go, LayerMask layer)
    {
        return layer == (layer | 1 << go.layer);
    }
}

