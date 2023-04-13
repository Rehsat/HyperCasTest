using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.Storages
{
    public class StorageWidget : MonoBehaviour
    {
        [SerializeField] private bool _showOnlyOnMax;
        [SerializeField] private Storage _storage;
        [SerializeField] private Text _text;

        [SerializeField] private string _onMaxText;

        private void OnEnable()
        {
            _storage.OnStorablesCountChange += UpdateText;
            UpdateText(_storage.CurrentStorablesCount, _storage.MaxStorablesInStorage);
        }

        private void UpdateText(int currentValue, int maxValue)
        {
            var isMax = currentValue >= maxValue;

            if (_showOnlyOnMax && isMax == false)
                _text.gameObject.SetActive(false);
            else
                _text.gameObject.SetActive(true);

            var text = isMax ? _onMaxText : $"{currentValue}/{maxValue}";
            _text.text = text;
        }

        private void OnDisable()
        {
            _storage.OnStorablesCountChange -= UpdateText;
        }
    }
}
