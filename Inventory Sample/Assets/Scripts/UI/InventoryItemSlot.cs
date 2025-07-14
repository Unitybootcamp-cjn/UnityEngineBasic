using Gpm.Ui;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlotData : InfiniteScrollData
{
    public ItemDisplayData DisplayData { get; }
    public long ItemSerialNumber { get; }

    public InventoryItemSlotData(ItemDisplayData disaplyData, long serialNumber)
    {
        DisplayData = disaplyData ?? throw new ArgumentNullException(nameof(disaplyData));
        ItemSerialNumber = serialNumber;
    }
}

public class InventoryItemSlot : InfiniteScrollItem
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _grade;

    private InventoryItemSlotData _data;

    public override void UpdateData(InfiniteScrollData scrollData)
    {
        base.UpdateData(scrollData);

        _data = scrollData as InventoryItemSlotData;

        _icon.sprite = _data.DisplayData.IconSprite;
        _grade.sprite = _data.DisplayData.GradeSprite;
    }

    [SerializeField] private EquipmentUI _equipmentUI;

    public void OnClickSlot()
    {
        var data = new EquipmentUIData(
            data: _data.DisplayData,
            serialNumber: _data.ItemSerialNumber,
            isEquipped: false
        );
        _equipmentUI.Open(data);
    }
}
