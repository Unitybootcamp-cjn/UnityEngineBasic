using System.Collections;
using UnityEngine;

// MonoBehaviour : 개발자가 Unity engine 의 GameObject에 직접 백엔드 로직을 부여하는 컴포넌트를 추가하고 싶을 때 작성하는 단위.
public class Test_EventFunctions : MonoBehaviour
{
    /// <summary>
    /// 처음 이 Component를 포함하는 GameObject가 (Prefab) 생성되어 로드될 때 호출됨.
    /// -> 처음 이 Component가 AddComponent 를 통해 로드될 때 호출됨.
    /// Unity의 Monobehaviour 는 개발자가 직접 생성자 오버로드를 쓰는 것이 허용되지 않음.
    /// 생성자를 못 쓰기 때문에 이 Monobehaviour 를 초기화하는 로직은 Awake 에서 작성한다.
    /// </summary>
    private void Awake()
    {
        Debug.Log("<color=Red>[Awake]</color> : ");
    }

    /// <summary>
    /// * 이 컴포넌트를 가지는 GameObject가 활성화 되어 있을 때만 *
    /// 이 컴포넌트가 활성화 될 때마다 호출
    /// </summary>
    private void OnEnable()
    {
        Debug.Log("<color=Brown>[OnEnable]</color> : ");
    }

    /// <summary>
    /// * 이 컴포넌트를 가지는 GameObject가 활성화 되어 있을 때만 *
    /// 이 컴포넌트가 비활성화 될 때마다 호출
    /// </summary>
    private void OnDisable()
    {
        Debug.Log("<color=Brown>[OnDisable]</color> : ");
    }

    /// <summary>
    /// 아직 이 컴포넌트가 Update를 한 번도 수행하지 않았을 때 Update 호출 직전에 한 번 호출
    /// 현재 씬에 있는 모든 GameObject 들이 초기화 되고 난 후, 게임로직 시작 직전에 다른 GameObject들과 상호작용하기 위해 초기화할 게 있다면 여기 작성
    /// </summary>
    void Start()
    {
        Debug.Log("<color=Blue>[Start]</color> : ");
        IEnumerator routine = C_CountRoutine();
        StartCoroutine(routine); // 코루틴으로 이 Enumerator 를 등록할테니, Yield instruction 에 맞게 루틴을 수행해달라고 함
        Debug.Log("<color=Blue>[Start]</color> : 코루틴 등록함");
    }


    /// <summary>
    /// Unity 에서 코루틴을 작성하려면 IEnumerator Yield instruction을 작성하여
    /// Monobehaviour 에 등록해서 enumerator 의 현재 명령(객체) 에 맞는 타이밍에 로직을 수행하고 MoveNext로 다음 로직을 실행할 수 있도록 대기시킬 수 있다.
    /// 
    /// 코루틴 왜쓰냐?
    /// 여태 C#에서 기본적으로 작성해본 함수들은 다 동기 함수다. (어떤 함수 호출하면 그 함수 내용 끝날때까지 다른 함수 수행하지 않음)
    /// 유니티 이벤트 사이클의 타이밍에 맞는 비동기 로직을 작성하기 위해서 사용
    /// </summary>
    /// <returns></returns>
    IEnumerator C_CountRoutine()
    {
        Debug.Log("<color=cyan>[코루틴]</color> 진입함. 1초 대기할거임");
        yield return new WaitForSeconds(1);

        Debug.Log("<color=cyan>[코루틴]</color> 1초 대기 끝남. Update 기다릴거임");
        yield return null;

        Debug.Log("<color=cyan>[코루틴]</color> FixedUpdate 기다릴거임");
        yield return new WaitForFixedUpdate();

        Debug.Log("<color=cyan>[코루틴]</color> FixedUpdate 끝남.");
        Destroy(this.gameObject);
    }

    /// <summary>
    /// 매 프레임 마다 수행할 로직 작성
    /// -> 이 이벤트 함수의 부하가 너무 높으면 프레임 업데이트 주기가 길어져서 초당프레임이 떨어짐(렉걸림)
    /// </summary>
    void Update()
    {
        Debug.Log("<color=Green>[Update]</color> :");
    }
    /// <summary>
    /// update 로직 및 애니메이션 수행 완료 후에 해당 프레임의 정확한 Transform 을 반영하기 위한 로직 등에 주로 사용(ex. Camera)
    /// </summary>
    private void LateUpdate()
    {
        
    }

    /// <summary>
    /// 고정 프레임 단위 호출(시간 변화에 민감한 연산들, 특히 물리연산)
    /// </summary>
    private void FixedUpdate()
    {
        Debug.Log("<color=Yellow>[FixedUpdate]</color> :");
    }

    private void OnDestroy()
    {
        Debug.Log("<color=Red>[OnDestroy]</color> :");
    }

    // Editor 상에서 현재 Component 의 모든 데이터를 초기값으로 되돌리는 함수
    // 처음에 이 Component 를 GameObject에 붙여넣을때(AddComponent) 호출됨.
    // 개발자가 직접 호출할 수도 있다.
    private void Reset()
    {

    }

    private void OnApplicationFocus(bool focus)
    {
        
    }

    private void OnApplicationPause(bool pause)
    {
        
    }

    private void OnApplicationQuit()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(Vector3.zero,Vector3.one);
    }
}
