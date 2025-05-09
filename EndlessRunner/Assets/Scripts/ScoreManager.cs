using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    public PlayerController playerController;
    public BossController bossController;
    public DeadManager deadManager;   //���� �Ŵ���

    //����
    //1. �ΰ��� ������ �����ϰ� ����ҰŸ� �ʵ�� �����.
    //2. ������ ���� �� �ڿ��� ������ �����Ǿ� �ϴ� ��Ȳ�� �����
    //   ������ ���� ������� ����մϴ�.
    //   2-1. PlayerPrefs : ����Ƽ�� �����͸� ������Ʈ���� ������ �� ����մϴ�.
    //                      ����,�Ǽ�,���� ������ ������ ������ ���� ����
    //                      ���� ���ž��ϸ� ������ �����ص� �����ִ� ��찡 �����.

    //   2-2. Json        : JavaScript Object Notation : ������ ���ۿ� ����
    //                      ��ü, �迭, ���ڿ� , Boolean, Null, ���� ���� ������ ���� ����
    //                      �ַ� ���ӿ��� ��ųʸ��� ����Ʈ ���� ���ؼ� ������ ���̺�, �κ��丮
    //                      ���� ���̺� ���� �����ҷ��� �� �� ���ϰ� ����ϴ� �뵵

    //   2-3. Firebase    : �����ͺ��̽����� ������ ���� �����͸� �����մϴ�.(��Ƽ ����)

    //   2-4. ScriptableObject : ����Ƽ ���ο� Asset�� ���·ν� �����͸� �����ؼ� ����մϴ�.
    //                           �ΰ��� �����͸� ������ �� ���� ���� ���մϴ�.

    //   2-5. CSV         : ���� ���Ͽ� �ʿ��� �����͵��� �����صΰ�, C# ��ũ��Ʈ�� ���� �ش� ���� ���ͼ�
    //                      �����մϴ�. �ַ� �� ���� , ��ũ��Ʈ ��ȭ ȣ��, �⺻���� ������
    private float score = 0.0f;

    //������ ���� ���̵� ǥ��
    private int level = 1;

    //�ִ� ����
    private int max_level = 10;

    //���� �� �䱸 ����
    private int levelperscore = 10;

    //�ؽ�Ʈ UI
    public TMP_Text scoreText;
    //public TextMeshProUGUI scoreText;
    public TMP_Text levelText;    //���� ����
    public TMP_Text perScoreText; //���� ���������� ����
    public TMP_Text Player_Speed; //�÷��̾��� �̵� �ӵ�
    public TMP_Text HighScore;

    //���� ���� üũ
    [SerializeField] private bool DeadCheck = false;


    private void Start()
    {
        //HIGH_SCORE Ű�� ���ٸ�, Ű�� ���� ����
        if (PlayerPrefs.HasKey("HIGH_SCORE") == false)
        {
            Debug.Log("HIGH_SCORE Ű�� ���ŵǾ����ϴ�.");
            PlayerPrefs.SetInt("HIGH_SCORE", 0);
        }
        else
        {
            Debug.Log("Ű�� �����մϴ�!");
        }

        //�ְ� ���� ����
        HighScore.text = $"High Score : {PlayerPrefs.GetInt("HIGH_SCORE")}";

        SetTMP_Text();
    }
    //String Format $
    // $"{����}"�� ���� ��� �ش� ������ ���ڿ��� �Ѿ�� �˴ϴ�. 
    // {����:N0} : ���� ��� �� ,�� ǥ���� �� �ֽ��ϴ�. 1000 -> 1,000 
    // #,##0 : #�� 0�� �տ� �Ⱥ���. ���ڰ� ������ ǥ��, ������ ǥ�� ����
    //         0�� �տ� ����. ���ڰ� ������ ����ǥ��, ������ 0 ǥ��

    void Update()
    {
        //���� ������ ��� ���ھ ���̻� ������ �ʽ��ϴ�.
        //�÷��̾�� UI���� ���� �������� ó���ǰ� ���� �����
        //static ������ �����ͷ� ó���ϴ� �͵� �������ϴ�.
        if (DeadCheck)
        {
            return;
        }

        if (score >= levelperscore)
        {
            LevelUP();
        }

        score += 3 * Time.deltaTime;

        scoreText.text = ((int)score).ToString();

        //score�� ���̽��ھ� ���� �Ѿ��� ����� �ؽ�Ʈ ����
        if (score > PlayerPrefs.GetInt("HIGH_SCORE"))
        {
            //�ش� �ڵ带 ����ϸ� ��� ������ ���� �����Ǳ� ������ ����� �����ְ�, Dead���� ���� 1������ ó��
            //PlayerPrefs.SetInt("HIGH_SCORE", (int)score);
            //HighScore.text = $"High Score : {PlayerPrefs.GetInt("HIGH_SCORE")}"; 
            HighScore.text = $"High Score : " + ((int)score).ToString();
        }

    }
    private void LevelUP()
    {
        if (level == max_level)
            return;

        levelperscore *= 2;
        level++;
        playerController.SetSpeed(2);
        bossController.SetSpeed(2);
        SetTMP_Text();
    }
    public void SetTMP_Text()
    {
        levelText.text = $"Level :  {level}";
        perScoreText.text = $"Next Level : {levelperscore: #,##0}";
        Player_Speed.text = $"Speed : {playerController.GetSpeed()}";
    }

    public void OnDead()
    {
        Debug.Log("OnDead");
        DeadCheck = true;
        //�Ŵ����� ���� ����
        deadManager.SetScoreText(score);
    }
}