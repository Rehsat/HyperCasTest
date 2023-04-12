using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour, IStorablesContainer
{
    // TODO: Попытаться разделить на MVC
    [SerializeField] private bool _isHorizontal;
    [SerializeField] private int _maxStorablesInStorage;
    [SerializeField] private int _maxInRow;
    [SerializeField] private Vector3 _spacing;
    [SerializeField] private Transform _startStoragePostion;

    private Vector3 _standartSpacing;
    private Vector3 _rowOverflowSpacing;

    private Vector3 _lastStorablePlacePosition;
    
    private Stack<Storable> _storables = new Stack<Storable>();
    
    public int CurrentStorablesCount => _storables.Count;

    private void Start()
    {
        
        _standartSpacing = _isHorizontal ? 
            new Vector3(_spacing.x, 0, 0) : new Vector3(0, _spacing.y, 0);
        
        _rowOverflowSpacing = _isHorizontal ? 
            new Vector3(0, _spacing.y, 0) : new Vector3(_spacing.x, 0, 0);
    }

    public void AddStorable(Storable storable)
    {
        storable.transform.parent = _startStoragePostion;
        storable.StartMoveToPoint(GetNextStoragePosition());
        _storables.Push(storable);
    }

    public Storable GetStorable()
    {
        return _storables.Pop();
    }

    public Vector3 GetNextStoragePosition()
    {
        var rowNumber = _storables.Count / _maxInRow;
       // var lastStorablePlacePosition = _storables.Count > 0 ? 
         //   _storables.Peek().transform.localPosition : Vector3.zero;

        var nextPosition = Vector3.zero +
                           (rowNumber*_rowOverflowSpacing +  _storables.Count%_maxInRow * _standartSpacing);
        return nextPosition;
    }

    public int GetEmptySLotsCount()
    {
        return _maxStorablesInStorage - CurrentStorablesCount;
    }

    public List<Storable> GetStorables(int count)
    {
        var list = new List<Storable>();
        for(int i =0; i <count; i++)
        {
            if(_storables.Count == 0) break;
            var lastPickUpedStorable = _storables.Pop();
            list.Add(lastPickUpedStorable);
        }

        return list;
    }
}