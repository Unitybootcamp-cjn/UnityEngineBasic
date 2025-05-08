using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    //��Ʈ�� ��ư�� ���ؼ� �������� �޴�
    public GameObject ControlKeyMenu;
    //���� �ȳ��� �޴�
    public GameObject ExitKeyMenu;

    public void OnStartButtonEnter()
    {
        //GameScene ������ �ε��մϴ�.
        SceneManager.LoadScene("GameScene");
    }

    public void OnControlKeyButtonEnter()
    {
        //activeSelf�� ������ ���� ������Ʈ�� Ȱ�� ���������� ���θ� Ȯ���� �� ����
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

    //������ �� ȯ�濡���� �����
    //���� �� ȯ�濡���� ���Ḧ ��Ȳ�� ���� ó���մϴ�.
    public void OnExitButtonEnter()
    {
#if UNITY_EDITOR // ����Ƽ ������ �ʿ����� �۾�
        UnityEditor.EditorApplication.isPlaying = false;
        //������ �ٷ� ������ ���(�����, �����)
#else
        Application.Quit(); // ���� ��Ȱ��ȭ�Ǵ� �ڵ尡 �ٷ� ����
#endif
    }
}
