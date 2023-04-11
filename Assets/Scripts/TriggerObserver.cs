using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObserver : MonoBehaviour
{
    [SerializeField] private LayerMask _observableLayer;
    public Action<Collider> OnEnterTrigger { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.IsInLayer(_observableLayer))
            OnEnterTrigger?.Invoke(other);
    }
}
public static class GameObjectExtensions 
{
    public static bool IsInLayer(this GameObject go, LayerMask layer)
    {
        return layer == (layer | 1 << go.layer);
    }
}

