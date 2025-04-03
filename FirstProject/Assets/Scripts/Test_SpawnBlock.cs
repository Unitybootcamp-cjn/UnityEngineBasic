/*
 * Reflection : 런타임중에 메타데이터에 접근할 수 있는 기능
 * 
 * Attribute : 특성데이터를 추가하기 위한 클래스
 */
using System;
using System.Reflection;
using UnityEngine;

public class Test_SpawnBlock : MonoBehaviour
{
    /*
     * SerializeField Attribute
     * 이 특성이 부여된 필드를 특정 텍스트 포맷으로 변환하여 Inspector 창에 노출시키는 특성
     * 
     * Serialize 직렬화 : 객체를 특정 데이터포맷(텍스트 / 바이트배열 등..)으로 바꾸는 과정
     * Deserialize 역직렬화 : 특정 데이터포맷을 객체로 바꾸는 과정
     */

    [SerializeField] [Trash] GameObject _basicBlockRed;
    [SerializeField] GameObject _basicBlockOrange;
    [SerializeField] GameObject _basicBlockYellow;
    [SerializeField] GameObject _basicBlockGreen;
    [SerializeField] [Trash] GameObject _basicBlockBlue;
    [SerializeField] GameObject _basicBlockPurple;


    private void Awake()
    {
        Type type = this.GetType(); // 현재 객체의 타입에 대한 메타데이터 객체 참조 가져옴
        FieldInfo[] fieldInfos = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

        for (int i = 0; i < fieldInfos.Length; i++)
        {
            TrashAttribute attribute = fieldInfos[i].GetCustomAttribute<TrashAttribute>();

            if (attribute != null)
                Debug.Log($"{fieldInfos[i].Name} 은 쓰레기로 분류되었다. ");
        }
    }

    private void Start()
    {
        // Component 들은 직접 생성자를 호출하는게 아니고, GameObject.AddComponent를 통해서 내부적으로 생성하는 것이기 때문에
        // Prefab 으로 복사본 인스턴스를 만들고 싶다면 UnityEngine.Object.Instantiate() 를 호출해야 한다.
        Instantiate(_basicBlockRed); // Game World 에 인스턴스 생성

        Transform target = GetComponent<Transform>(); // 현재 GameObject 가 가지고 있는 컴포넌트들 중 Transform 타입을 찾아서 반환
        Instantiate(_basicBlockOrange, target);

        Instantiate(_basicBlockYellow, transform); // transform 프로퍼티 : 현재 GameObject 에 캐싱된 Transform 을 반환
        Instantiate(_basicBlockGreen);
        Instantiate(_basicBlockBlue);
        Instantiate(_basicBlockPurple);
    }
}
