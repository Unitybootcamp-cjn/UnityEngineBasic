using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadManager : MonoBehaviour
{
    public TMP_Text current_score;
    public TMP_Text high_score;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    //�ش� �޴��� Ȱ��ȭ �Ǿ��� �� �ڵ�
    public void OnEnable()
    {
        Debug.Log("Ȱ��ȭ ����");
    }
    public void SetScoreText(float score)
    {
        Debug.Log("SetScoreText");
        gameObject.SetActive(true);
        current_score.text = "���� : " + ((int)(score)).ToString();

        //���࿡ ���޹��� ������ ���� ������Ʈ���� ���̽��ھ�� ũ�ٸ�
        if (score > PlayerPrefs.GetInt("HIGH_SCORE"))
        {
            //���Ӱ� �����մϴ�.
            PlayerPrefs.SetInt("HIGH_SCORE", (int)score);
        }

        high_score.text = "�ְ� ���� : " + PlayerPrefs.GetInt("HIGH_SCORE").ToString();
    }


    //�ش� �޴��� ��Ȱ��ȭ�Ǿ��� �� �ڵ�
    public void OnDisable()
    {
        Debug.Log("��Ȱ��ȭ ����");
    }

    public void OnReplayButtonEnter()
    {
        //GetActiveScene()�� ���� Ȱ��ȭ�Ǿ��ִ� ���� �ǹ��մϴ�.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnTitleButtonEnter()
    {
        SceneManager.LoadScene("TitleScene");
    }
}