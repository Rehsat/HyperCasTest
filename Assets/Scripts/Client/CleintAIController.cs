using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CleintAIController : MonoBehaviour
{
    [SerializeField] private Stall _stall;

    [SerializeField] private CashRegister _cashRegister;
  //  [SerializeField] private 
    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.SetDestination(_stall.transform.position);
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        _navMeshAgent.SetDestination(_stall.transform.position);
        yield return new WaitForSeconds(7);
        _navMeshAgent.SetDestination(_cashRegister.transform.position);
        yield return new WaitForSeconds(20);
        _navMeshAgent.SetDestination(_stall.transform.position);
    }
}

