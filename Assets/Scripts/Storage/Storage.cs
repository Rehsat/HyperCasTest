using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shop.Storages
{

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
        public int MaxStorablesInStorage => _maxStorablesInStorage;
        public Action<int, int> OnStorablesCountChange;

        public void Init(int maxCount)
        {
            _maxStorablesInStorage = maxCount;
            foreach (var storabe in _storables.ToArray())
            {
                Destroy(storabe.gameObject);
            }
            OnStorablesCountChange?.Invoke(CurrentStorablesCount, MaxStorablesInStorage);
            _storables.Clear();
        }

        private void Start()
        {
            OnStorablesCountChange?.Invoke(CurrentStorablesCount, MaxStorablesInStorage);
            _standartSpacing = _isHorizontal ? new Vector3(_spacing.x, 0, 0) : new Vector3(0, _spacing.y, 0);

            _rowOverflowSpacing = _isHorizontal ? new Vector3(0, _spacing.y, _spacing.z) : new Vector3(_spacing.x, 0, 0);
        }

        public void AddStorable(Storable storable)
        {
            storable.StartMoveToPoint(_startStoragePostion, GetNextStoragePosition());
            _storables.Push(storable);
            OnStorablesCountChange?.Invoke(CurrentStorablesCount, MaxStorablesInStorage);
        }

        public Storable GetStorable()
        {
            return _storables.Pop();
        }

        public Vector3 GetNextStoragePosition()
        {
            var rowNumber = _storables.Count / _maxInRow;
            var nextPosition = Vector3.zero +
                               (rowNumber * _rowOverflowSpacing + _storables.Count % _maxInRow * _standartSpacing);
            return nextPosition;
        }

        public int GetEmptySLotsCount()
        {
            return _maxStorablesInStorage - CurrentStorablesCount;
        }

        public List<Storable> GetStorables(int count)
        {
            var list = new List<Storable>();
            for (int i = 0; i < count; i++)
            {
                if (_storables.Count == 0) break;
                var lastPickUpedStorable = _storables.Pop();
                list.Add(lastPickUpedStorable);
            }

            OnStorablesCountChange?.Invoke(CurrentStorablesCount, MaxStorablesInStorage);
            return list;
        }
    }
}
