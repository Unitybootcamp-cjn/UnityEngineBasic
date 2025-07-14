using Gpm.Common.ThirdParty.MessagePack.Resolvers;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public sealed class EquipmentUIData
{
    public ItemDisplayData DisplayData { get; }
    public long ItemSerialNumber { get; set; }
    public bool IsEquipped { get; }

    public EquipmentUIData(ItemDisplayData data, long serialNumber, bool isEquipped)
    {
        DisplayData = data ?? throw new ArgumentNullException(nameof(data));
        ItemSerialNumber = serialNumber;
        IsEquipped = isEquipped;
    }
}

public class EquipmentUI : MonoBehaviour
{
    [Header("Service Locator")]
    [SerializeField] private UserInventoryServiceLocatorSO _userInventoryServiceLocator;

    [Header("UI Elements")]
    [SerializeField] private Image _gradeImage;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _gradeText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _processButtonLabel;

    public event Action OnProcessButtonClicked;

    private EquipmentUIData _data;

    public void Open(EquipmentUIData data)
    {
        gameObject.SetActive(true);

        _data = data ?? throw new ArgumentNullException(nameof(data));
        
        _gradeImage.sprite = _data.DisplayData.GradeSprite;
        _iconImage.sprite = _data.DisplayData.IconSprite;
        _gradeText.text = GetGradeText(_data.DisplayData.Grade);
        _nameText.text = _data.DisplayData.Name;

        if (_data.IsEquipped)
        {
            _processButtonLabel.text = "Unequip";
        }
        else
        {
            _processButtonLabel.text = "Equip";
        }

        static string GetGradeText(ItemGrade grade)
        {
            return grade switch
            {
                ItemGrade.Common => "<color=blue>Common</color>",
                ItemGrade.Uncommon => "<color=green>Uncommon</color>",
                ItemGrade.Rare => "<color=purple>Rare</color>",
                ItemGrade.Epic => "<color=orange>Epic</color>",
                ItemGrade.Legendary => "<color=red>Legendary</color>",
                _ => throw new ArgumentOutOfRangeException(nameof(grade), grade, "Invalid item grade")
            };
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void OnClickProcessButton()
    {
        if (_data.IsEquipped)
        {
            _userInventoryServiceLocator.Service.Unequip(_data.ItemSerialNumber);
        }
        else
        {
            _userInventoryServiceLocator.Service.Equip(_data.ItemSerialNumber);
        }

        OnProcessButtonClicked?.Invoke();
        OnProcessButtonClicked = null;

        Close();
    }
}
