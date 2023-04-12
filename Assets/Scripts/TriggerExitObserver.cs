using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExitObserver : MonoBehaviour
{
    [SerializeField] private LayerMask _observableLayer;
    public Action<Collider> OnExitTrigger { get; set; }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.IsInLayer(_observableLayer))
            OnExitTrigger?.Invoke(other);
    }
}
