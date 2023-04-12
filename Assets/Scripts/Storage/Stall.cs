using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stall : MonoBehaviour
{
    [SerializeField] private FruitData _fruitData;
    [SerializeField] private Image _tabletSprite;

    private void Start()
    {
        _tabletSprite.sprite = _fruitData.Sprite;
    }
}
