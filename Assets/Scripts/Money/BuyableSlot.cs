using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shop.Money
{
    [RequireComponent(typeof(CashCollector))]
    public class BuyableSlot : MonoBehaviour
    {
        [SerializeField] private int _cost;
        [SerializeField] private Storable _dollar;
        [SerializeField] private GameObject _buyable;
        [SerializeField] private Transform _buyableCreatePosition;
        [SerializeField] private TriggerEnterObserver _observer;

        private CashCollector _buyerCashCollector;
        private CashCollector _cashCollector;

        private const float TIME_TO_GET_MONEY = 0.1f;

        public Action<GameObject> OnBought;

        private void Awake()
        {
            _cashCollector = GetComponent<CashCollector>();
            _cashCollector.AddCash(-_cost);

            _observer.OnEnterTrigger += StartBuy;
        }

        private void StartBuy(Collider potentialBuyer)
        {
            if (_buyerCashCollector != null || potentialBuyer.TryGetComponent(out _buyerCashCollector))
            {
                StartCoroutine(Buy());
            }
        }

        private void OnDestroy()
        {
            _observer.OnEnterTrigger -= StartBuy;
        }

        private IEnumerator Buy()
        {
            while (_buyerCashCollector.TryGetCash())
            {

                var dollar = Instantiate(_dollar, _buyerCashCollector.transform.position, Quaternion.identity);
                dollar.StartMoveToPoint(transform, Vector3.down, () => { dollar.gameObject.SetActive(false); });
                _cashCollector.AddCash();
                if (_cashCollector.Container.CashCount <= 0)
                {
                    var buyable = Instantiate(_buyable, _buyableCreatePosition.position, _buyable.transform.rotation);
                    OnBought?.Invoke(buyable);
                    Destroy(gameObject);
                    break;
                }

                yield return new WaitForSeconds(TIME_TO_GET_MONEY);
            }
        }

    }
}


