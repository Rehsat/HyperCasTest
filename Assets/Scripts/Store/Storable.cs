using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Storable : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private Vector3 _pointToMove;
    public void StartMoveToPoint(Vector3 point)
    {
        StopAllCoroutines();
        _pointToMove = point;
        StartCoroutine(MoveToPoint());
    }

    public abstract int GetId();

    private IEnumerator MoveToPoint()
    {
        while (Vector3.Distance(transform.position, _pointToMove) > 0.02f)
        {
            yield return null;
            transform.position = Vector3.Lerp
                (transform.position, _pointToMove, _moveSpeed);
        }
    }
    
    
}
