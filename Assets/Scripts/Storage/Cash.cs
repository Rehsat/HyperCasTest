using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shop.Money
{


    public class Cash : Storable
    {
        [SerializeField] private int _cost = 1;

        public int Cost => _cost;

        public override int GetId()
        {
            return 2;
        }
    }
}
