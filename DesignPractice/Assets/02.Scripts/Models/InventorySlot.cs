using System;
using UnityEngine;

namespace DP.Models
{
    /// <summary>
    /// InventorySlot ��
    /// </summary>
    [Serializable] // ����������ڷ����� Serializable Attribute�� �߰��ؼ� ����ȭ�� ������ �ڷ����̶�� ���� ���
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
            // �� �����̸� ���� ����
            if (slot == Empty)
            {
                return new InventorySlot(pair.itemId, pair.itemNum);
            }
            else
            {
                // �ٸ� ������ ���� �߰��ϸ� ����
                if (slot.ItemId != pair.itemId)
                    throw new Exception("�ٸ� ������ ������ ���Ϸ��� �õ���");

                return new InventorySlot(slot.ItemId,slot.ItemNum + pair.itemNum); // ������ ���� ����
            }
        }

        public static InventorySlot operator -(InventorySlot slot, (int itemId, int itemNum) pair)
        {
            if (slot.ItemId != pair.itemId)
                throw new Exception("�ٸ� ������ ������ ������ �õ���");

            if (slot.ItemId <= 0)
                throw new Exception("�������� ���µ� ������ ������ ����");

            if (slot.ItemNum - pair.itemNum < 0)
                throw new Exception("���� �����ʹ� ������ ������ ���� �� ����");

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
