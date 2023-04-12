using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegister : MonoBehaviour
{
    [SerializeField] private TriggerEnterObserver _clientQueueObserver;
    [SerializeField] private TriggerEnterObserver _playerEnterObserver;
    [SerializeField] private TriggerExitObserver _playerExitObserver;
    
    [SerializeField] private Transform _boxSpawnPosition;
    [SerializeField] private Box _boxPrefab;
    [SerializeField] private Dollar _dollarPrefab;
    [SerializeField] private Storage _moneyStorage;

    private bool _isReadyToServe;
    private Storage _playerStorage;
    private Queue<Storage> _clients = new Queue<Storage>();

    private void OnEnable()
    {
        _playerEnterObserver.OnEnterTrigger += StartServe;
        _playerExitObserver.OnExitTrigger += StopServe;
        _clientQueueObserver.OnEnterTrigger += TryAddClient;
    }

    private void TryAddClient(Collider potentialClient)
    {
        Debug.LogError(potentialClient.gameObject.name);
        if (potentialClient.TryGetComponent<Storage>(out var client))
        {
            Debug.LogError(2);
            _clients.Enqueue(client);
        }
        
    }

    private void StartServe(Collider other)
    {
        
        if(_clients.Count == 0) return;
        _isReadyToServe = true;
        StartCoroutine(Serve());
    }

    private void StopServe(Collider other)
    {
        _isReadyToServe = false;
    }

    private IEnumerator Serve()
    {
        while (_clients.Count > 0)
        {
            var client = _clients.Dequeue();
            var box = Instantiate(_boxPrefab, _boxSpawnPosition.position, Quaternion.identity);
            var moneyCount = 0;
            
            yield return new WaitForSeconds(1.5f);
            while (client.CurrentStorablesCount>0)
            {
                var storable = client.GetStorable();
                box.Storage.AddStorable(storable);
                if (storable is Fruit fruit)
                    moneyCount += fruit.FruitData.Cost;
                yield return new WaitForSeconds(1f);
            }

            for(int i =0; i<moneyCount;i++)
            {
                var money = Instantiate(_dollarPrefab, client.transform.position, Quaternion.identity);
                _moneyStorage.AddStorable(money);
            }
            client.AddStorable(box);
        }
    }

    private void OnDisable()
    {
        _playerEnterObserver.OnEnterTrigger -= StartServe;
        _playerExitObserver.OnExitTrigger -= StopServe;
        _clientQueueObserver.OnEnterTrigger -= TryAddClient;
    }
}
