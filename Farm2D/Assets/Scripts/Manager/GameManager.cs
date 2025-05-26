using UnityEngine;

public class GameManager : MonoBehaviour
{
    //������ �Ŵ���
    public ItemManager ItemManager;
    public TileManager TileManager;

    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        ItemManager = GetComponent<ItemManager>();
        TileManager = GetComponent<TileManager>();
    }
    #endregion


}
