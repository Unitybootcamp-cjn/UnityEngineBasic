using UnityEngine;
using UnityEngine.UI;
using System;

public class EquipmentSlotData
{
    public ItemDisplayData DisplayData { get; }
    public long SerialNumber { get; }
    public InventoryUI InventoryUI { get; }

    public EquipmentSlotData(ItemDisplayData displayData, long serialNumber, InventoryUI inventoryUI)
    {
        DisplayData = displayData ?? throw new ArgumentNullException(nameof(displayData));
        SerialNumber = serialNumber;
        InventoryUI = inventoryUI;
    }
}

public class EquipmentSlot : MonoBehaviour
{
    [Header("Connected UI")]
    [SerializeField] private EquipmentUI _equipmentUI;

    [Header("UI Elements")]
    [SerializeField] private GameObject ItemUI;
    [SerializeField] private Image GradeImage;
    [SerializeField] private Image IconImage;

    private EquipmentSlotData _data;

    private void Start()
    {
        Unequip();
    }

    public void Equip(EquipmentSlotData data)
    {
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        _data = data;

        ItemUI.SetActive(true);
        GradeImage.sprite = _data.DisplayData.GradeSprite;
        IconImage.sprite = _data.DisplayData.IconSprite;
    }

    public void Unequip()
    {
        ItemUI.SetActive(false);
    }

    public void OnClickSlot()
    {
        var data = new EquipmentUIData(
            data: _data.DisplayData,
            serialNumber: _data.SerialNumber,
            isEquipped: true
        );
        _equipmentUI.OnProcessButtonClicked += () =>
        {
            Unequip();
            _data.InventoryUI.Refresh();
        };
        _equipmentUI.Open(data);
    }
}
