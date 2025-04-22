using System.Collections.Generic;
using UnityEngine;

namespace DP.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Scriptable Objects/itemLookupTable")]
    public class ItemLookupTable : ScriptableObject
    {
        public Item this[int itemId]
        {
            get
            {
                EnsureInitialized();

                if (_lookup.TryGetValue(itemId, out Item item))
                    return item;

                return null;
            }
        }


        [SerializeField] Item[] _items;
        Dictionary<int, Item> _lookup;
        bool _initialized = false;

        private void OnEnable()
        {
            _initialized = false;
            EnsureInitialized();
        }

        void EnsureInitialized()
        {
            if(_initialized) 
                return;

            InitializeLookup();
        }
        void InitializeLookup()
        {
            _lookup = new Dictionary<int, Item>(_items.Length);

            for (int i = 0; i < _items.Length; i++)
            {
                _lookup.Add(_items[i].Id, _items[i]);
            }

            _initialized = true;
        }
    }
}