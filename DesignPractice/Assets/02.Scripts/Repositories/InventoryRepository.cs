using System.Collections.Generic;
using DP.Contexts;
using DP.Models;

namespace DP.Repositories
{
    public class InventoryRepository
    {
        /// <summary>
        /// �̱��� : ���α׷� ���ۺ��� ������ �ν��Ͻ��� �� �Ѱ��� ����� �� Ȥ�� �ַξ��� �Ѱ��� ���� ������ �ʿ��� ��
        /// ���� ����� ������ �� �ְ� �ʱ�ȭ�س��� ����
        /// </summary>
        public static InventoryRepository Singleton
        {
            get
            {
                // �ν��Ͻ� ������ ������ �ش�
                if (_singleton == null)
                {
                    _singleton = new InventoryRepository(new InventoryContext());
                }

                return _singleton;
            }
        }

        private static InventoryRepository _singleton;

        /// <summary>
        /// �����ڸ� ���� ���ؽ�Ʈ ������ ����
        /// </summary>
        public InventoryRepository(InventoryContext context)
        {
            _context = context;
        }

        InventoryContext _context;

        /// <summary>
        /// �κ��丮�� ��� ���� ������ �б�
        /// </summary>
        public IEnumerable<InventorySlot> GetAllSlots()
            => _context.Inventory.Slots;

        /// <summary>
        /// Ư�� ��ġ�� �κ��丮 ���� �б�
        /// </summary>
        /// <param name="slotIndex"> �а� ���� ���� ��ġ </param>
        public InventorySlot GetSlot(int slotIndex)
            => _context.Inventory.Slots[slotIndex];

        /// <summary>
        /// ������ �߰�.
        /// ������ �������� �̹� ���Կ� �ִٸ� �ش� ��ġ�� ������ �߰�
        /// ���Կ� �����ٸ� �� ������ ã�Ƽ� �ش� ��ġ�� ������ ������ �߰�
        /// </summary>
        /// <param name="itemId"> �߰��ϰ� ���� ������ ���̵� </param>
        /// <param name="itemNum"> �߰��ϰ� ���� ������ ���� </param>
        public void AddItem(int itemId, int itemNum)
        {
            int slotIndex = _context.Inventory.Slots.FindIndex(slot => slot.ItemId == itemId);

            if (slotIndex < 0)
                slotIndex = _context.Inventory.Slots.FindIndex(slot => slot == InventorySlot.Empty);

            if (slotIndex < 0)
                throw new System.Exception("�� ������ ���µ� �߰� �Լ� ȣ���");

            _context.Inventory.Slots[slotIndex] += (itemId, itemNum);
        }

        public void RemoveItem(int itemId, int itemNum)
        {
            int slotIndex = _context.Inventory.Slots.FindIndex(slot => slot.ItemId == itemId);

            if(slotIndex < 0)
                throw new System.Exception("���� ������ ������� ��");

            if (_context.Inventory.Slots[slotIndex].ItemNum < itemNum)
                throw new System.Exception("������ �ִ� �� ���� �� ���� ������� ��");

            _context.Inventory.Slots[slotIndex] -= (itemId, itemNum);
        }

        public void Save()
            => _context.Save();
    }
}