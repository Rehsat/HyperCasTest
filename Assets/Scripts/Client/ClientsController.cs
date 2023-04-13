using System;
using System.Collections;
using System.Collections.Generic;
using Shop.Storages;
using UnityEditor.PackageManager;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Shop.Client
{

    public class ClientsController : MonoBehaviour
    {
        // в будущем надо сделать список и из него рандомно выбирать фрукт, но пока он у нас 1
        [SerializeField] private FruitData _fruitData;

        [SerializeField] private List<ClientAIController> _clients;
        [SerializeField] private List<Storage> _storages;
        [SerializeField] private CashRegister _cashRegister;
        [SerializeField] private TriggerEnterObserver _outPosition;

        private void OnEnable()
        {
            _outPosition.OnEnterTrigger += InitClient;
        }

        public void Init()
        {
            foreach (var client in _clients)
            {
                InitClient(client);
            }
        }

        public void AddStorage(Storage storage)
        {
            _storages.Add(storage);
        }

        private void InitClient(Collider potentialClient)
        {
            if (potentialClient.TryGetComponent<ClientAIController>(out var client))
            {
                InitClient(client);
            }
        }

        private void InitClient(ClientAIController client)
        {
            var randomCountOfFruits = Random.Range(2, 5);
            client.Init(randomCountOfFruits, _storages, _fruitData, _outPosition.transform, _cashRegister);
        }

        private void OnDisable()
        {
            _outPosition.OnEnterTrigger -= InitClient;
        }

        private void Start()
        {
            Init();
        }
    }
}
