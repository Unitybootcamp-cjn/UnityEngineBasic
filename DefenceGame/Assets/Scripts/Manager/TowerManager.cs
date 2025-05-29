using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    #region Singleton
    public static TowerManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }
    #endregion

    //타워 전용 풀
    public List<TowerData> towerPool;

    public Transform towerParent;

    public void RandSpawnTower(Vector2 spawnPos)
    {
        TowerData towerData = GetTower();

        if (GameManager.instance.Cost(towerData.cost))
        {
            GameObject tower = Instantiate(towerData.prefab, spawnPos, Quaternion.identity);

            Debug.Log($"{towerData.tower_name}이 생성되었습니다!");
        }
        else
        {
            Debug.Log("잔액이 부족합니다");
        }
    }

    private TowerData GetTower()
    {
        float total_weight = 0;
        //타워 풀에 등록된 타워 데이터를 대상으로 총합 가중치를 계산합니다.
        foreach(TowerData data in towerPool)
        {
            total_weight += data.weight;
        }

        float rand = Random.Range(0, total_weight); //0부터 총합 가중치 사이의 랜덤 값 설정
        float current = 0; //누적 가중치

        foreach(TowerData tower in towerPool)
        {
            current += tower.weight;
            //누적된 가중치가 랜덤 값보다 크거나 같은 상황이 오면 해당 타워를 return 합니다.
            if(rand <= current)
            {
                return tower;
            }
        }

        //위 작업에서 처리가 안된 상황이면 무조건 기본 타워가 나오도록 설계(오류 방지)
        return towerPool[0];
    }
}
