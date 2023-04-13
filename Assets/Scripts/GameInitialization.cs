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

            foreach (var buyable in _buyableSlots)
            {
                buyable.OnBought += TryAddStorage;
            }
        }

        private void TryAddStorage(GameObject potentialStorage)
        {
            if (potentialStorage.TryGetComponent<Storage>(out var storage))
            {
                _clientsController.AddStorage(storage);
            }
        }

        private void OnDestroy()
        {
            foreach (var buyable in _buyableSlots)
            {
                buyable.OnBought -= TryAddStorage;
            }

        }
    }
}
