using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] private bool _isHorizontal;
    [SerializeField] private int _maxStorablesInStorage;
    [SerializeField] private int _maxInRow;
    [SerializeField] private Vector3 _spacing;
    [SerializeField] private Transform _startStoragePostion;

    private Stack<Storable> _storables = new Stack<Storable>();
    
    public int CurrentStorablesCount => _storables.Count;
    
    public void AddStorable(Storable storable)
    {
        storable.StartMoveToPoint(GetNextStoragePosition());
        _storables.Push(storable);
    }

    public Storable GetStorable()
    {
        return _storables.Pop();
    }

    public Vector3 GetNextStoragePosition()
    {
        return _startStoragePostion.position + _spacing * _storables.Count;
    }
}
