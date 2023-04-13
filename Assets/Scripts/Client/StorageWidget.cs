using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageWidget : MonoBehaviour
{
    [SerializeField] private Storage _storage;
    [SerializeField] private Text _text;
    
    [SerializeField] private string _onMaxText;

    private void OnEnable()
    {
        _storage.OnStorablesCountChange +=
    }

    private void UpdateText(int currentValue, int maxValue)
    {
        var text = currentValue >= maxValue ? _onMaxText : $"{currentValue}/{maxValue}";
        _text.text = text;
    }
}
