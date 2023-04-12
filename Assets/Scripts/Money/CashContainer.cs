using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CashContainer
{
    private int _cashCount;

    public int CashCount
    {
        get => _cashCount;
        set
        {
            _cashCount = value;
            OnCashCountChanged?.Invoke(_cashCount);
        } 
    }

    public Action<int> OnCashCountChanged;
}
