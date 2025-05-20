using System.Collections;
using Assets.Scripts.Dialog;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageController : MonoBehaviour
{
    public static StageController Instance; //�Ŵ��� ���� ����

    public int StagePoint = 0; //����

    public Text PointText;   //������ ǥ���� UI
    public Text QuestText;   //����Ʈ�� ǥ���� UI
    public QuestData QuestData; //����� ����Ʈ ������
    public Image fade_Image;
    public float duration = 5.0f;

    public bool accecptQuest = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

        //���̵� ��
        if (fade_Image != null)
        {
            fade_Image.gameObject.SetActive(true);
            Color c = fade_Image.color;
            fade_Image.color = new Color(c.r, c.g, c.b, 1.0f);
            StartCoroutine(FadeIn());
        }

        //�ȳ��� ������ �ݹ�
        DialogDataAlert alert = new DialogDataAlert("START", "10�ʸ��� �����Ǵ� �����ӵ��� �����ϼ���.",
            () =>
            {
                Debug.Log("OK ��ư�� �����ּ���.");
            });

        DialogManager.Instance.Push(alert);


    }

    IEnumerator FadeIn()
    {
        yield return StartCoroutine(Fade(1, 0));
    }

    IEnumerator FadeOut()
    {
        yield return StartCoroutine(Fade(0, 1));
    }


    IEnumerator Fade(float start, float end)
    {
        float time = 0.0f;
        Color s = new Color(0, 0, 0, start);
        Color e = new Color(0, 0, 0, end);

        while (time < duration)
        {
            fade_Image.color = Color.Lerp(s, e, time / duration);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        fade_Image.color = e;
    }



    //������ �����ϸ�, �ؽ�Ʈ UI�� ��ġ�� ����
    public void AddPoint(int Point)
    {
        StagePoint += Point;
        PointText.text = Point.ToString();
    }

    //���� ���� ���ε�
    public void FinishGame()
    {
        DialogDataConfirm confirm = new DialogDataConfirm("Restart?", "Please press OK if you want to restart the game"
            ,
            delegate (bool answer)
            {
                if (answer)
                {
                    StartCoroutine(FadeOut());
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                else
                {
                    //���� ������Ʈ�� ������ Ȱ���ؼ� ������ �󿡼��� ����ǵ��� �������ּ���.
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                }
            }
         );

        //�Ŵ����� ���
        DialogManager.Instance.Push(confirm);
    }


    public void OnQuest()
    {

        if (accecptQuest)
        {
            DialogDataAlert alert = new DialogDataAlert("�ȳ���", "�̹� ����Ʈ�� ���� �����Դϴ�!");
            DialogManager.Instance.Push(alert);
            return;
        }

        //     //����Ʈ �ݹ�
        DialogDataQuest quest = new DialogDataQuest("����Ʈ ����", "����Ʈ�� �����Ͻðڽ��ϱ�?", QuestData,
    delegate (bool answer)
    {
        if (answer)
        {
            accecptQuest = true;
            SetData();
        }
        else
        {
            Debug.Log("����Ʈ ���� x");
        }
    });
        DialogManager.Instance.Push(quest);
    }

    public void SetData()
    {

        QuestText.text = $"{QuestData.title}";
        QuestText.text += "\n" + QuestData.description;
        QuestText.text += $"\n���� ���� ���� �� : {QuestData.currentCount} / {QuestData.GoalCount} ";
        QuestText.text += $"\n ����Ʈ ���� {QuestData.Reward}";
    }
}