using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Storable
{
    [SerializeField] private FruitData _fruitData;

    public override int GetId()
    {
        return _fruitData.Id;
    }
}
