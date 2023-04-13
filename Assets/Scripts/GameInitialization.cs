using System.Collections.Generic;
using Shop.Client;
using Shop.Money;
using Shop.Storages;
using UnityEngine;

namespace Shop
{

    public class GameInitialization : MonoBehaviour
    {
        [SerializeField] private InputObserver _inputObserver;
        [SerializeField] private ClientsController _clientsController;

        [SerializeField] private List<MonoBehaviour> _listeners;
        [SerializeField] private List<BuyableSlot> _buyableSlots;

        private BuyableObserver _buyableObserver;
        private void Awake()
        {
            var inputListeners = new List<IInputListener>();
            foreach (var listener in _listeners)
            {
                if (listener is IInputListener inputListener)
                {
                    inputListeners.Add(inputListener);
                }
            }

            _inputObserver.Init(inputListeners);
            _buyableObserver = new BuyableObserver(_clientsController, _buyableSlots);
        }
        

        private void OnDestroy()
        {
            _buyableObserver.Clear();
        }
    }

    public class BuyableObserver
    {
        private ClientsController _listener;
        private List<BuyableSlot> _buyables;
        public BuyableObserver(ClientsController listener, List<BuyableSlot> buyables)
        {
            _listener = listener;
            _buyables = buyables;
            foreach (var buyable in _buyables)
            {
                buyable.OnBought += TryAddStorage;
            }
        }
        private void TryAddStorage(GameObject potentialStorage)
        {
            if (potentialStorage.TryGetComponent<Storage>(out var storage))
            {
                _listener.AddStorage(storage);
            }
        }
        public void Clear()
        {
            foreach (var buyable in _buyables)
            {
                buyable.OnBought -= TryAddStorage;
            }
            
        }
    }
}