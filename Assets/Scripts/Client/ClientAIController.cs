using System;
using System.Collections;
using System.Collections.Generic;
using Shop.Storages;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Shop.Client
{


    [RequireComponent(typeof(NavMeshAgent), typeof(Storage))]
    public class ClientAIController : MonoBehaviour
    {
        private Stall _stall;

        private CashRegister _cashRegister;

        //  [SerializeField] private 
        private NavMeshAgent _navMeshAgent;
        private Storage _storage;

        private int _itemsNeedCount;
        private Transform _outPosition;
        private FruitData _fruitData;
        private List<Storage> _itemStorages;
        private IntentionType _currentIntention;

        public FruitData FruitData => _fruitData;

        public IntentionType CurrentIntention
        {
            get => _currentIntention;
            set
            {

                OnIntentionChanged?.Invoke(value);
                _currentIntention = value;
            }
        }

        public Action<IntentionType> OnIntentionChanged;

        public void Init(int itemsNeedCount, List<Storage> itemStorages, FruitData fruitData, Transform outPosition,
            CashRegister cashRegister)
        {
            if (_navMeshAgent == null)
                _navMeshAgent = GetComponent<NavMeshAgent>();
            if (_storage == null)
                _storage = GetComponent<Storage>();
            _storage.Init(itemsNeedCount);
            _itemStorages = itemStorages;
            _fruitData = fruitData;
            _outPosition = outPosition;
            _cashRegister = cashRegister;

            var randomStallId = Random.Range(0, _itemStorages.Count);
            var randomStall = _itemStorages[randomStallId];
            ChangeIntention(IntentionType.GetItems, MoveToItems(), randomStall.transform);
        }




        private void ChangeIntention(IntentionType intentionType, IEnumerator intention = null,
            Transform agentDestination = null)
        {
            StopAllCoroutines();

            if (intention != null)
                StartCoroutine(intention);
            if (agentDestination != null)
                _navMeshAgent.SetDestination(agentDestination.position);

            CurrentIntention = intentionType;
        }


        private IEnumerator MoveToItems()
        {
            while (_storage.CurrentStorablesCount < _storage.MaxStorablesInStorage)
            {
                yield return null;
            }

            ChangeIntention(IntentionType.Pay, agentDestination: _cashRegister.transform);
        }


        public void StartMoveToOut()
        {
            ChangeIntention(IntentionType.MoveOut, agentDestination: _outPosition);
        }

    }

    public enum IntentionType
    {
        GetItems,
        Pay,
        MoveOut
    }
}

