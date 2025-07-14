using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    public int wave_count = 0; // 웨이브 카운트
    public int gold = 100; //시작 골드

    public void Earn(int amount)
    {
        gold += amount;
        //UI 매니저를 통해 화면의 골드 텍스트에 대한 처리
        UIManager.instance.UpdateGoldUI(gold);
    }

    public bool Cost(int amount)
    {
        //지불할 코스트가 없다면 false
        if (gold < amount)
            return false;
        //일반적인 경우, 코스트를 지불하고 true
        gold -= amount;
        UIManager.instance.UpdateGoldUI(gold);
        return true;
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER");
        //다시 시작할 수 있는 기능을 만들어주거나, 종료 기능
    }

}
