using UnityEngine;

[CreateAssetMenu(menuName = "Tower/TowerData")]
public class TowerData : ScriptableObject
{
    public Sprite icon;     //이미지
    public string tower_name;//타워 이름
    public float damage;    //데미지
    public float range;     //공격 범위
    public float attack_speed;//공격 속도(주기)
    public int cost;        //구매 비용
    public GameObject prefab;//생성할 타워 오브젝트
    public float weight = 1.0f; //가중치(특정 유닛이 잘 나오도록)


}
