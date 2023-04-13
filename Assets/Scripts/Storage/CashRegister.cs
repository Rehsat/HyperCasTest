using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Shop.Client;
using Shop.Money;
using Shop.Storages;
using UnityEngine;

namespace Shop
{

    public class CashRegister : MonoBehaviour
    {
        [SerializeField] private TriggerEnterObserver _clientQueueObserver;
        [SerializeField] private TriggerEnterObserver _playerEnterObserver;
        [SerializeField] private TriggerExitObserver _playerExitObserver;

        [SerializeField] private Transform _boxSpawnPosition;
        [SerializeField] private Box _boxPrefab;
        [SerializeField] private Cash _dollarPrefab;
        [SerializeField] private Storage _moneyStorage;

        private bool _isReadyToServe;
        private bool _isServing;
        private CashCollector _playerCashCollector;
        private Queue<Storage> _clients = new Queue<Storage>();

        private void OnEnable()
        {
            _playerEnterObserver.OnEnterTrigger += StartServe;
            _playerExitObserver.OnExitTrigger += StopServe;
            _clientQueueObserver.OnEnterTrigger += TryAddClient;
        }

        private void TryAddClient(Collider potentialClient)
        {
            if (potentialClient.TryGetComponent<Storage>(out var client))
            {
                _clients.Enqueue(client);
            }

        }

        private void StartServe(Collider other)
        {
            if (_playerCashCollector == null)
            {
                _playerCashCollector = other.GetComponent<CashCollector>();
            }
            SendCashToCollector();
            if (_clients.Count == 0) return;
            _isReadyToServe = true;
            StartCoroutine(Serve());
        }

        private void StopServe(Collider other)
        {
            _isReadyToServe = false;
        }

        private void SendCashToCollector()
        {
            while (_moneyStorage.CurrentStorablesCount != 0)
            {
                var storable = _moneyStorage.GetStorable();
                storable.StartMoveToPoint(_playerCashCollector.transform,
                    _playerCashCollector.Transform.localPosition, () =>
                    {
                        _playerCashCollector.AddCash();
                        Destroy(storable.gameObject);
                    });
            }
        }

        //TODO: Убрать магические числа
        private IEnumerator Serve()
        {
            while (_clients.Count > 0)
            {
                var client = _clients.Dequeue();
                var box = Instantiate(_boxPrefab, _boxSpawnPosition.position, Quaternion.identity);
                var moneyCount = 0;

                yield return new WaitForSeconds(0.5f);
                while (client.CurrentStorablesCount > 0)
                {
                    var storable = client.GetStorable();
                    box.Storage.AddStorable(storable);
                    if (storable is Fruit fruit)
                        moneyCount += fruit.FruitData.Cost;
                    yield return new WaitForSeconds(0.5f);
                }

                for (int i = 0; i < moneyCount; i++)
                {
                    var money = Instantiate(_dollarPrefab, client.transform.position, Quaternion.identity);
                    _moneyStorage.AddStorable(money);
                }

                client.AddStorable(box);
                client.GetComponent<ClientAIController>().StartMoveToOut();
                yield return new WaitForSeconds(1f);
                if (_isReadyToServe)
                {
                    SendCashToCollector();
                }

            }
        }

        private void OnDisable()
        {
            _playerEnterObserver.OnEnterTrigger -= StartServe;
            _playerExitObserver.OnExitTrigger -= StopServe;
            _clientQueueObserver.OnEnterTrigger -= TryAddClient;
        }
    }
}
