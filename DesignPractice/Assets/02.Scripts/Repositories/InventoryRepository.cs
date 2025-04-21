using System.Collections.Generic;
using DP.Contexts;
using DP.Models;

namespace DP.Repositories
{
    public class InventoryRepository
    {
        /// <summary>
        /// 싱글톤 : 프로그램 시작부터 끝까지 인스턴스를 딱 한개만 사용할 때 혹은 주로쓰는 한개에 대한 참조만 필요할 때
        /// 정적 멤버로 참조할 수 있게 초기화해놓는 패턴
        /// </summary>
        public static InventoryRepository Singleton
        {
            get
            {
                // 인스턴스 없으면 만들어서라도 준다
                if (_singleton == null)
                {
                    _singleton = new InventoryRepository(new InventoryContext());
                }

                return _singleton;
            }
        }

        private static InventoryRepository _singleton;

        /// <summary>
        /// 생성자를 통해 컨텍스트 의존성 주입
        /// </summary>
        public InventoryRepository(InventoryContext context)
        {
            _context = context;
        }

        InventoryContext _context;

        /// <summary>
        /// 인벤토리의 모든 슬롯 데이터 읽기
        /// </summary>
        public IEnumerable<InventorySlot> GetAllSlots()
            => _context.Inventory.Slots;

        /// <summary>
        /// 특정 위치의 인벤토리 슬롯 읽기
        /// </summary>
        /// <param name="slotIndex"> 읽고 싶은 슬롯 위치 </param>
        public InventorySlot GetSlot(int slotIndex)
            => _context.Inventory.Slots[slotIndex];

        /// <summary>
        /// 아이템 추가.
        /// 동일한 아이템이 이미 슬롯에 있다면 해당 위치에 갯수를 추가
        /// 슬롯에 없었다면 빈 슬롯을 찾아서 해당 위치에 아이템 데이터 추가
        /// </summary>
        /// <param name="itemId"> 추가하고 싶은 아이템 아이디 </param>
        /// <param name="itemNum"> 추가하고 싶은 아이템 갯수 </param>
        public void AddItem(int itemId, int itemNum)
        {
            int slotIndex = _context.Inventory.Slots.FindIndex(slot => slot.ItemId == itemId);

            if (slotIndex < 0)
                slotIndex = _context.Inventory.Slots.FindIndex(slot => slot == InventorySlot.Empty);

            if (slotIndex < 0)
                throw new System.Exception("빈 슬롯이 없는데 추가 함수 호출됨");

            _context.Inventory.Slots[slotIndex] += (itemId, itemNum);
        }

        public void RemoveItem(int itemId, int itemNum)
        {
            int slotIndex = _context.Inventory.Slots.FindIndex(slot => slot.ItemId == itemId);

            if(slotIndex < 0)
                throw new System.Exception("없는 아이템 지우려고 함");

            if (_context.Inventory.Slots[slotIndex].ItemNum < itemNum)
                throw new System.Exception("가지고 있는 것 보다 더 많이 지우려고 함");

            _context.Inventory.Slots[slotIndex] -= (itemId, itemNum);
        }

        public void Save()
            => _context.Save();
    }
}