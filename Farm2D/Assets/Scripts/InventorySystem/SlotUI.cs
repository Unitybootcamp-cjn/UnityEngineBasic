using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Assets.Scripts.InventorySystem
{
    public class SlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		public Image slot_icon;
		public Text slot_count_text;
		public Text desc;

		//슬롯 설정
		public void SetSlot(Slot slot)
		{
			if(slot != null)
			{
				slot_icon.sprite = slot.icon;
				slot_count_text.text = slot.count.ToString();
			}
		}

		//슬롯 비우기
		public void SetEmpty()
		{
			slot_icon.sprite = null;
			slot_count_text.text = "";
		}

        public void OnPointerEnter(PointerEventData eventData)
        {
            desc.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            desc.gameObject.SetActive(false);
        }
    }
}