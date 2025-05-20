using UnityEngine;



[CreateAssetMenu(menuName = "Quest/QuestData")]
public class QuestData : ScriptableObject
{
    public string title;
    public string description;
    public int currentCount;
    public int GoalCount;
    public string Reward;
}