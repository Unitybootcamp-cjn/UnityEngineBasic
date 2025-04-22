/*
 * ScriptableObject
 * 
 * 쓰기 / 읽기가 가능한 데이터컨테이너 에셋을 만들기 위한 자료형
 * 
 * 사용처
 * 1. 에디터상에서만 밸런스테스트용
 * 2. 런타임중에 빌드한 에셋을 활용
 */

using UnityEngine;
using UnityEngine.UI;

namespace DP.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
    public class Item : ScriptableObject
    {
        [field: SerializeField] public int Id { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}

