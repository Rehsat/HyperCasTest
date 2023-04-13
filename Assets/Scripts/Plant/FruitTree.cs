using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Object = System.Object;

namespace Shop.Storages
{
    public class FruitTree : MonoBehaviour, IStorablesContainer
    {
        [SerializeField] private Transform _fruitStartSpawnPosition;
        [SerializeField] private Fruit _fruit;
        [SerializeField] private FruitGenerator _fruitGenerator;
        [SerializeField] private List<Transform> _plantsPosition;

        private int _currentPositition;
        private FruitFactory _fruitFactory;
        private Stack<Fruit> _activeFruits = new Stack<Fruit>();



        private void OnEnable()
        {
            _fruitGenerator.Init(_plantsPosition.Count);
            _fruitFactory = new FruitFactory(_fruit, _fruitStartSpawnPosition.position);
            _fruitGenerator.OnPlantsGenerated += MoveGeneratedFruitsToPoints;
            _fruitGenerator.StartGenerate();
        }

        private void MoveGeneratedFruitsToPoints(int generatedFruitsCount)
        {
            var fruit = _fruitFactory.GetFruit();
            _activeFruits.Push(fruit);
            var position = _plantsPosition[_currentPositition];
            fruit.StartMoveToPoint(transform, position.localPosition);
            _currentPositition++;
        }

        private void OnDisable()
        {
            _fruitGenerator.OnPlantsGenerated -= MoveGeneratedFruitsToPoints;
        }

        public List<Storable> GetStorables(int count)
        {
            var list = new List<Storable>();
            for (int i = 0; i < count; i++)
            {
                if (_activeFruits.Count == 0)
                    break;

                var fruit = _activeFruits.Pop();
                _fruitGenerator.TakeFruit();
                _currentPositition--;
                list.Add(fruit);
            }

            _fruitGenerator.StartGenerate();
            return list;
        }
    }

    public class FruitFactory
    {
        private Fruit _prefab;
        private Vector3 _spawnPosition;

        public FruitFactory(Fruit prefab, Vector3 spawnPosition)
        {
            _prefab = prefab;
            _spawnPosition = spawnPosition;
        }

        public Fruit GetFruit()
        {
            var instantinate = UnityEngine.Object.Instantiate(_prefab, _spawnPosition, Quaternion.identity);
            return instantinate;
        }
    }
}