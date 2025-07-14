using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private GameConfigurationSO _config;

    [SerializeField] private ItemRepositoryLocatorSO _itemRepositoryLocator;
    [SerializeField] private ItemServiceLocatorSO _itemServiceLocator;
    [SerializeField] private UserInventoryServiceLocatorSO _userInventoryServiceLocator;
    [SerializeField] private UserInventoryRepositoryLocatorSO _userInventoryRepositoryLocator;
    [SerializeField] private ItemDisplayDataFactoryLocatorSO _itemDisplayDataFactoryLocator;

    private void Awake()
    {
        IReadonlyStorage<ItemModelList> parser = new ResourcesJsonParser<ItemModelList>();
        _itemRepositoryLocator.Bootstrap(parser, _config.ItemFilePath);

        var userInventoryFilePath = Path.Combine(Application.persistentDataPath, _config.UserInventoryFilePath);
        _userInventoryRepositoryLocator.Bootstrap(userInventoryFilePath);

        _itemServiceLocator.Bootstrap(_itemRepositoryLocator.Repository);
        _itemDisplayDataFactoryLocator.Bootstrap(_itemServiceLocator.Service);
        _userInventoryServiceLocator.Bootstrap(
            _userInventoryRepositoryLocator.Repository,
            _itemServiceLocator.Service);
    }
}
