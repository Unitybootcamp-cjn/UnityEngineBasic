using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DP.ScriptableObjects;

namespace DP.UIs
{
    public class UI_InventorySlot : MonoBehaviour
    {
        public int index {  get; set; }


        [SerializeField] Image _itemIcon;
        [SerializeField] TextMeshProUGUI _itemNum;
        [SerializeField] ItemLookupTable _itemLookupTable;


        public void Render(int itemId, int itemNum)
        {
            _itemIcon.sprite = _itemLookupTable[itemId]?.Icon;
            _itemNum.text = (itemNum > 0) ? itemNum.ToString() : string.Empty;
        }
    }
}

