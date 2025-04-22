using System.Collections.Generic;
using DP.Models;
using DP.Repositories;
using DP.UIs;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace DP.Controllers
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] InputActionReference _rightClick;
        [SerializeField] GraphicRaycaster _graphicRaycaster;
        PointerEventData _pointerEventData;
        List<RaycastResult> _results = new List<RaycastResult>();
        InventoryRepository _inventoryRepository;
        int _rightDownSlotUIIndex = -1;


        private void Awake()
        {
            _pointerEventData = new PointerEventData(EventSystem.current);
        }

        private void Start()
        {
            _inventoryRepository = InventoryRepository.Singleton;
        }

        private void OnEnable()
        {
            _rightClick.action.performed += OnRightClick;
            _rightClick.action.Enable();
        }

        private void OnDisable()
        {
            _rightClick.action.performed -= OnRightClick;
            _rightClick.action.Disable();
        }

        void OnRightClick(InputAction.CallbackContext context)
        {

            // 슬롯을 클릭했는지
            if (TryCastSlotUI(out UI_InventorySlot slotUI) == false)
                return;

            // 눌린거면 눌린 슬롯 인덱스만 기억
            if (context.ReadValueAsButton())
            {
                _rightDownSlotUIIndex = slotUI.index;
                return;
            }
            else
            {
                // 눌렸던 슬롯이랑 떼는 슬롯이랑 다르면 아이템 사용 하면 안됨
                if (_rightDownSlotUIIndex != slotUI.index)
                {
                    _rightDownSlotUIIndex = -1;
                    return;
                }
            }
            _rightDownSlotUIIndex = -1;

            // 클릭된 슬롯이 빈 슬롯인지
            InventorySlot slot = _inventoryRepository.GetSlot(slotUI.index);

            if (slot == InventorySlot.Empty)
                return;

            // 빈 슬롯 아니니까 아이템 사용 (갯수 1 차감)
            _inventoryRepository.RemoveItem(slot.ItemId, 1);
            _inventoryRepository.Save();
        }

        bool TryCastSlotUI(out UI_InventorySlot slotUI)
        {
            _results.Clear(); // 캐스팅 결과 캐시 지움
            _pointerEventData.position = Mouse.current.position.value; // 현재 마우스 위치에 발생한 이벤트 데이터 초기화
            _graphicRaycaster.Raycast(_pointerEventData,_results); // 마우스이벤트데이터로 캐스팅

            // 뭔가 캐스팅 된게 있다
            if(_results.Count > 0 )
            {
                // slot UI 가 캐스팅 되었다
                if (_results[0].gameObject.TryGetComponent(out slotUI))
                {
                    return true;
                }
            }
            slotUI = null;
            return false;
        }
    }
}