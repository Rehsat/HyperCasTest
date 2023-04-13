using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Storable : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    
    private Vector3 _pointToMove;
    private Action _callback;
    
    public void StartMoveToPoint(Transform point, Action onGotDestination = null)
    {
        StartMoveToPoint(point.parent, point.position, onGotDestination);
    }

    public void StartMoveToPoint(Vector3 point, Action onGotDestination = null)
    {
        StartMoveToPoint(null, point, onGotDestination);
    }

    public void StartMoveToPoint(Transform parrent, Vector3 point, Action onGotDestination = null)
    {
        StopAllCoroutines();
        transform.parent = parrent;
        _pointToMove = point;
        _callback = onGotDestination;
        StartCoroutine(MoveToPoint());
    }

    public abstract int GetId();

    private IEnumerator MoveToPoint()
    {
        while (Vector3.Distance(transform.localPosition, _pointToMove) > 0.01f)
        {
            yield return null;
            transform.localPosition = Vector3.Lerp
                (transform.localPosition, _pointToMove, _moveSpeed);
        }
        
        _callback?.Invoke();
    }
    
    
}
