using System;
using UnityEngine;
using UnityEngine.UI;


public class CashWidget :  MonoBehaviour
{
    [SerializeField] private CashCollector _cashCollector;
    [SerializeField] private Text _cashCountText;

    private void OnEnable()
    {
        _cashCollector.Container.OnCashCountChanged += UpdateCashCountText;
    }

    private void UpdateCashCountText(int cashCount)
    {
        _cashCountText.text = cashCount.ToString();
    }

    private void OnDisable()
    {
        _cashCollector.Container.OnCashCountChanged -= UpdateCashCountText;
    }
}
