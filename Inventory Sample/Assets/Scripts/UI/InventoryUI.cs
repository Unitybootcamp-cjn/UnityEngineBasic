using Gpm.Ui;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using System;

public class InventoryUIData
{
    public UserInventoryItemModel ItemModel { get; }
    public ItemDisplayData DisplayData { get; }

    public InventoryUIData(UserInventoryItemModel model, ItemDisplayDataFactory displayDataFactory)
    {
        ItemModel = model;
        DisplayData = displayDataFactory.Create(model.ItemId);
    }
}

public class InventoryUI : MonoBehaviour
{
    [Header("Factory Locator")]
    [SerializeField] private ItemDisplayDataFactoryLocatorSO _itemDisplayDataFactoryLocator;

    [Header("Service Locator")]
    [SerializeField] private UserInventoryServiceLocatorSO _userInventoryServiceLocator;
    [SerializeField] private ItemServiceLocatorSO _itemServiceLocator;

    [Header("UI Elements")]
    [SerializeField] private InfiniteScroll _scroll;
    [SerializeField] private TextMeshProUGUI _sortButtonLabel;
    [SerializeField] private TextMeshProUGUI _orderButtonLabel;

    [Header("Equipment Slots")]
    [SerializeField] private EquipmentSlot _weaponSlot;
    [SerializeField] private EquipmentSlot _shieldSlot;
    [SerializeField] private EquipmentSlot _chestArmorSlot;
    [SerializeField] private EquipmentSlot _glovesSlot;
    [SerializeField] private EquipmentSlot _bootsSlot;
    [SerializeField] private EquipmentSlot _accessarySlot;

    private void Awake()
    {
        _equipmentSlots = new Dictionary<EquipSlotType, EquipmentSlot>
        {
            { EquipSlotType.Weapon, _weaponSlot },
            { EquipSlotType.Shield, _shieldSlot },
            { EquipSlotType.ChestArmor, _chestArmorSlot },
            { EquipSlotType.Gloves, _glovesSlot },
            { EquipSlotType.Boots, _bootsSlot },
            { EquipSlotType.Accessary, _accessarySlot }
        };
    }

    void Start()
    {
        CreateFilters();
        ChangeSortType(SortType.Grade);
        ChangeSortOrderType(SortOrderType.Descending);
        Refresh();
    }


    IReadOnlyList<InventoryUIData> _unequippedItems;
    private IReadOnlyDictionary<EquipSlotType, EquipmentSlot> _equipmentSlots;

    public void Refresh()
    {
        _unequippedItems = _userInventoryServiceLocator.Service.UnequippedItems
            .Select(model => new InventoryUIData(model, _itemDisplayDataFactoryLocator.Factory))
            .ToList();

        var equippedItems = _userInventoryServiceLocator.Service.EquippedItems;
        foreach (var item in equippedItems)
        {
            var uiData = new InventoryUIData(item.Value, _itemDisplayDataFactoryLocator.Factory);
            var slotData = new EquipmentSlotData(uiData.DisplayData, uiData.ItemModel.SerialNumber, this);

            _equipmentSlots[item.Key].Equip(slotData);
        }

        Sort();
        RenderUI();
    }

    private void RenderUI()
    {
        _scroll.ClearData();
        foreach (var item in _unequippedItems)
        {
            _scroll.InsertData(new InventoryItemSlotData(item.DisplayData, item.ItemModel.SerialNumber));
        }
    }

    enum SortType
    {
        Type,
        Grade
    }
    const int k_MaxSortType = 2;
    private SortType _sortType;

    private void ChangeSortType(SortType sortType)
    {
        _sortType = sortType;
        _sortButtonLabel.text = sortType switch
        {
            SortType.Type => "Type",
            SortType.Grade => "Grade",
            _ => throw new NotImplementedException($"SortType '{sortType}' is not implemented.")
        };
    }

    enum SortOrderType
    {
        Ascending,
        Descending
    }
    const int k_MaxSortOrderType = 2;
    private SortOrderType _sortOrderType;
    private void ChangeSortOrderType(SortOrderType sortOrderType)
    {
        _sortOrderType = sortOrderType;
        _orderButtonLabel.text = sortOrderType switch
        {
            SortOrderType.Ascending => "Asc",
            SortOrderType.Descending => "Desc",
            _ => throw new NotImplementedException($"sortOrderType '{sortOrderType}' is not implemented.")
        };
    }

    public void OnClickSortOrderType()
    {
        SortOrderType nextSortOrderType = (SortOrderType)(((int)_sortOrderType + 1) % k_MaxSortOrderType);
        ChangeSortOrderType(nextSortOrderType);
        Sort();
        RenderUI();
    }

    public void OnClickSortButton()
    {
        SortType nextSortType = (SortType)(((int)_sortType + 1) % k_MaxSortType);
        ChangeSortType(nextSortType);
        Sort();
        RenderUI();
    }

    private Func<InventoryUIData, int>[] _filters;

    private void CreateFilters()
    {
        _filters = new Func<InventoryUIData, int>[k_MaxSortType]
        {
            item => (int)(item.DisplayData.Type),  // Type
            item => (int)(item.DisplayData.Grade)   // Grade
        };
    }

    private void Sort()
    {
        Func<InventoryUIData, int> primaryKeySelector;
        Func<InventoryUIData, int> secondaryKeySelector;

        switch (_sortType)
        {
            case SortType.Grade:
                primaryKeySelector = _filters[(int)SortType.Grade];
                secondaryKeySelector = _filters[(int)SortType.Type];
                break;
            case SortType.Type:
                primaryKeySelector = _filters[(int)SortType.Type];
                secondaryKeySelector = _filters[(int)SortType.Grade];
                break;
            default:
                throw new NotImplementedException($"SortType '{_sortType}' is not implemented.");
        }

        IOrderedEnumerable<InventoryUIData> ordered;

        if (_sortOrderType == SortOrderType.Ascending)
        {
            ordered = _unequippedItems
                .OrderBy(primaryKeySelector)
                .ThenBy(secondaryKeySelector);
        }
        else
        {
            ordered = _unequippedItems
                .OrderByDescending(primaryKeySelector)
                .ThenByDescending(secondaryKeySelector);
        }

        _unequippedItems = ordered.ToList();
    }
}
