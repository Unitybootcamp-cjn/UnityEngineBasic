using System;
using UnityEngine;

namespace DP.Models
{
    /// <summary>
    /// InventorySlot 모델
    /// </summary>
    [Serializable] // 사용자정의자료형은 Serializable Attribute를 추가해서 직렬화가 가능한 자료형이라는 것을 명시
    public struct InventorySlot
    {
        public InventorySlot(int itemId, int itemNum)
        {
            ItemId = itemId;
            ItemNum = itemNum;
        }

        public static InventorySlot Empty => new InventorySlot(0, 0);

        public int ItemId;
        public int ItemNum;


        public static InventorySlot operator +(InventorySlot slot, (int itemId, int itemNum) pair)
        {
            // 빈 슬롯이면 새로 만듬
            if (slot == Empty)
            {
                return new InventorySlot(pair.itemId, pair.itemNum);
            }
            else
            {
                // 다른 아이템 종류 추가하면 예외
                if (slot.ItemId != pair.itemId)
                    throw new Exception("다른 아이템 종류를 더하려고 시도함");

                return new InventorySlot(slot.ItemId,slot.ItemNum + pair.itemNum); // 아이템 갯수 증가
            }
        }

        public static InventorySlot operator -(InventorySlot slot, (int itemId, int itemNum) pair)
        {
            if (slot.ItemId != pair.itemId)
                throw new Exception("다른 아이템 종류를 빼려고 시도함");

            if (slot.ItemId <= 0)
                throw new Exception("아이템이 없는데 개수를 뺄수는 없음");

            if (slot.ItemNum - pair.itemNum < 0)
                throw new Exception("슬롯 데이터는 갯수를 음수로 가질 수 없음");

            if (slot.ItemNum - pair.itemNum == 0)
                return InventorySlot.Empty;

            return new InventorySlot(slot.ItemId, slot.ItemNum - pair.itemNum);
        }


        public static bool operator ==(InventorySlot slot1, InventorySlot slot2)
            => (slot1.ItemId == slot2.ItemId) && (slot1.ItemNum == slot2.ItemNum);

        public static bool operator !=(InventorySlot slot1, InventorySlot slot2)
            => !(slot1 == slot2);
    }
}
