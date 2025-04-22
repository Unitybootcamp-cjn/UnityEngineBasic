using System.Collections.Generic;
using DP.Models;
using DP.Repositories;
using DP.UIs;
using UnityEngine;

namespace DP.Views
{
    // 인벤토리 뷰. 유저에게 보여줄 데이터의 최신값으로 UI의 시각적 요소를 갱신함
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] UI_InventorySlot _slotUIPrefab; // 인벤토리 슬롯 UI의 Prefab
        [SerializeField] RectTransform _slotUIContent; // 인벤토리 슬롯 UI를 넣게 될 Content 변수
        List<UI_InventorySlot> _slotUIs; // 인벤토리 슬롯 UI의 리스트
        
        
        private void Start()
        {
            SpawnSlotUIs();
        }

        // 이 컴포넌트가 활성화 될 때마다 호출
        private void OnEnable()
        {
            InventoryRepository.Singleton.OnInventoryChanged += Render;
        }

        // 이 컴포넌트가 비활성화 될 때마다 호출
        private void OnDisable()
        {
            InventoryRepository.Singleton.OnInventoryChanged -= Render;

        }


        // 
        public void Render(IEnumerable<InventorySlot> slots)
        {
            int slotIndex = 0;

             //인벤토리 슬롯 수가 런타임중 증가할 상황이 있을 때 가정:
            using (IEnumerator<InventorySlot> slotEnum = slots.GetEnumerator())
            using (IEnumerator<UI_InventorySlot> slotUIEnum = _slotUIs.GetEnumerator())
            {
                // 슬롯 데이터가 있음
                while (slotEnum.MoveNext())
                {
                    // 슬롯 UI 가 있음
                    if (slotUIEnum.MoveNext())
                    {
                        // 슬롯 데이터도 있고 만들어놓은 UI도 있으니 UI에 데이터 그리겠다.
                        slotUIEnum.Current.Render(slotEnum.Current.ItemId, slotEnum.Current.ItemNum);
                    }
                    // 슬롯 UI가 없으니까 새로 만들어서 데이터 그림
                    else
                    {
                        UI_InventorySlot slotUI = Instantiate(_slotUIPrefab, _slotUIContent);// 슬롯 데이터를 보여줄 게임오브젝트 생성
                        slotUI.index = slotIndex;// 생성된 슬롯UI의 인덱스에 slotIndex를 넣는다
                        slotUI.Render(slotEnum.Current.ItemId, slotEnum.Current.ItemNum);// 생성한 게임오브젝트 슬롯데이터로 그릴 내용 갱신
                        _slotUIs.Add(slotUI);// 인벤토리 슬롯 UI를 리스트에 삽입
                    }
                    slotIndex++;// 생성된 슬롯UI의 인덱스를 1씩 높혀가는 과정
                }
            }
        }

        // InventoryRepository의 슬롯을 모두 순회하여 UI로써 만들어내는 함수
        private void SpawnSlotUIs()
        {
            _slotUIs = new List<UI_InventorySlot>(); // 인벤토리슬롯UI의 리스트 변수를 생성
            int slotIndex = 0; // 슬롯 인덱스 0으로 초기화

            // 유저한테 보여줘야하는 데이터를 전부 순회.
            // InventoryRepository에 있는 슬롯을 모두 가져와 foreach로 순회한다
            foreach (InventorySlot slotData in InventoryRepository.Singleton.GetAllSlots())
            {
                UI_InventorySlot slotUI = Instantiate(_slotUIPrefab, _slotUIContent); // 슬롯 데이터를 보여줄 게임오브젝트 생성
                slotUI.index = slotIndex; // 생성된 슬롯UI의 인덱스에 slotIndex를 넣는다
                slotUI.Render(slotData.ItemId, slotData.ItemNum); // 생성한 게임오브젝트 슬롯데이터로 그릴 내용 갱신
                _slotUIs.Add(slotUI); // 인벤토리 슬롯 UI를 리스트에 삽입
                slotIndex++; // 생성된 슬롯UI의 인덱스를 1씩 높혀가는 과정
            }
        }
    }
}

