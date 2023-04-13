using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Storage))]
public class ClientAIController : MonoBehaviour
{
    [SerializeField] private Stall _stall;

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
            _currentIntention = value;
        }
    }

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
        Debug.LogError(itemStorages.Count);
        
        ChangeIntention(MoveToItems(), IntentionType.GetItems);
    }

    


    private void ChangeIntention(IEnumerator intention, IntentionType intentionType)
    {
        StopAllCoroutines();
        StartCoroutine(intention);
        
        CurrentIntention = intentionType;
    }
    

    private IEnumerator MoveToItems()
    {
        var randomStallId = Random.Range(0, _itemStorages.Count);
        var randomStall = _itemStorages[randomStallId];
        _navMeshAgent.SetDestination(randomStall.transform.position);
        while (_storage.CurrentStorablesCount < _storage.MaxStorablesInStorage)
        {
            yield return null;
        }
        
        _navMeshAgent.SetDestination(_cashRegister.transform.position);
    }

    
    public void StartMoveToOut()
    {
        _navMeshAgent.SetDestination(_outPosition.position);
    }
    
}

public enum IntentionType
{
    GetItems,
    Pay, 
    MoveOut
}

