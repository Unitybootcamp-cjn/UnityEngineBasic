using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    //컨트롤 버튼을 통해서 오픈해줄 메뉴
    public GameObject ControlKeyMenu;
    //종료 안내용 메뉴
    public GameObject ExitKeyMenu;

    public void OnStartButtonEnter()
    {
        //GameScene 씬으로 로드합니다.
        SceneManager.LoadScene("GameScene");
    }

    public void OnControlKeyButtonEnter()
    {
        //activeSelf는 현재의 게임 오브젝트가 활성 상태인지의 여부를 확인할 수 있음
        if(ControlKeyMenu.activeSelf == true)
        {
            ControlKeyMenu.SetActive(false);
        }
        else
        {
            ControlKeyMenu.SetActive(true);

        }
    }

    public void OnExitKeyButtonEnter()
    {
        if (ExitKeyMenu.activeSelf == true)
        {
            ExitKeyMenu.SetActive(false);
        }
        else
        {
            ExitKeyMenu.SetActive(true);
        }
    }

    //에디터 쪽 환경에서의 종료와
    //빌드 쪽 환경에서의 종료를 상황에 따라 처리합니다.
    public void OnExitButtonEnter()
    {
#if UNITY_EDITOR // 유니티 에디터 쪽에서의 작업
        UnityEditor.EditorApplication.isPlaying = false;
        //누르면 바로 꺼지는 기능(모바일, 빌드용)
#else
        Application.Quit(); // 현재 비활성화되는 코드가 바로 적용
#endif
    }
}
