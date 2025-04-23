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
        [SerializeField] InputActionReference _rightClick; // 우클릭 인풋을 받기 위한 변수
        [SerializeField] GraphicRaycaster _graphicRaycaster; // 레이캐스터를 받을 캠버스를 얻는 변수
        PointerEventData _pointerEventData; // 포인터(마우스,터치)에 대한 정보를 담고있는 데이터 객체 생성
        List<RaycastResult> _results = new List<RaycastResult>(); // RaycastResult란 EventSystem이 레이캐스트(Raycast) 를 했을 때 무엇을 맞췄는지에 대한 정보를 담고 있는 데이터 묶음
        InventoryRepository _inventoryRepository; // InventoryRepository 변수 생성
        int _rightDownSlotUIIndex = -1; // 우클릭 눌린 슬롯의 인덱스를 기억하기 위한 변수


        // 처음 이 Component를 포함하는 GameObject가 (Prefab) 생성되어 로드될 때 호출되는 Awake함수
        // 생성자를 못 쓰기 때문에 이 Monobehaviour 를 초기화하는 로직은 Awake 에서 작성함.
        private void Awake()
        {
            _pointerEventData = new PointerEventData(EventSystem.current); // 씬에 이벤트 시스템이 여러개 있을 수도 있어서 해당 입력이
                                                                           // 어떤 이벤트 시스템에 연결된 입력인지 명확히 해줘야함
                                                                           // 그걸 위한 EventSystem.current
        }


        // 아직 이 컴포넌트가 Update를 한 번도 수행하지 않았을 때 Update 호출 직전에 한 번 호출
        // 현재 씬에 있는 모든 GameObject 들이 초기화 되고 난 후,
        // 게임로직 시작 직전에 다른 GameObject들과 상호작용하기 위해 초기화할 게 있다면 여기 작성
        private void Start()
        {
            _inventoryRepository = InventoryRepository.Singleton; // InventoryRepository 가져옴
        }

        // 이 컴포넌트가 활성화 될 때마다 호출
        private void OnEnable()
        {
            _rightClick.action.performed += OnRightClick; 
            _rightClick.action.Enable();
        }

        // 이 컴포넌트가 비활성화 될 때마다 호출
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