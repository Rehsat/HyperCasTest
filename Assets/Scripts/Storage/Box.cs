using UnityEngine;

namespace Shop.Storages
{

    [RequireComponent(typeof(Storage))]
    public class Box : Storable
    {
        public Storage Storage { get; private set; }

        private void OnEnable()
        {
            Storage = GetComponent<Storage>();
        }

        public override int GetId()
        {
            return 1;
        }
    }
}
