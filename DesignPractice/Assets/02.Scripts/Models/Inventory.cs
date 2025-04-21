using System;
using System.Collections.Generic;

namespace DP.Models
{
    [Serializable]
    public class Inventory
    {
        public Inventory(int slotNum)
        {
            Slots = new List<InventorySlot>(slotNum);

            for (int i = 0; i < slotNum; i++)
                Slots.Add(InventorySlot.Empty);
        }


        public List<InventorySlot> Slots;
    }
}
