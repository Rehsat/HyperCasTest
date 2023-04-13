using UnityEngine;

public class CashCollector : MonoBehaviour
{
    
    [SerializeField] private bool _isReversed;
    [SerializeField] private TriggerEnterObserver _observer;
    private CashContainer _container = new CashContainer();
    
    
    public Transform Transform => _observer.transform;
    public CashContainer Container => _container;

   
    public void AddCash()
    {
        AddCash(1);
    }

    public void AddCash(int cashCount)
    {
        var modification = _isReversed ? -cashCount : cashCount;
        _container.CashCount += modification;
    }

    public bool TryGetCash()
    {
        var haveCash = _container.CashCount > 0;
        if (haveCash)
            _container.CashCount--;
        return haveCash;
    }
}
