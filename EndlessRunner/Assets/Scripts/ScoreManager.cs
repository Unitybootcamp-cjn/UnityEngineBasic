using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //����
    //1. �ΰ��� ������ �����ϰ� ����ҰŸ� �ʵ�� �����.
    //2. ������ ���� �� �ڿ��� ������ �����Ǿ� �ϴ� ��Ȳ�� �����
    //   ������ ���� ������� ����մϴ�.
    //   2-1. PlayerPrefs : ����Ƽ�� �����͸� ������Ʈ���� ������ �� ����մϴ�.
    //                      ����, �Ǽ�, ���� ������ ������ ������ ���� ����
    //                      ���� ���� ���ϸ� ������ �����ص� �����ִ� ��찡 �����.
    //   2-2. Json        : JavaScript Object Notation : ������ ���ۿ� ����
    //                      ��ü, �迭, ���ڿ�, Boolean, Null, ���� ���� ������ ���� ����
    //                      �ַ� ���ӿ��� ��ųʸ��� ����Ʈ ���� ���ؼ� ������ ���̺�, �κ��丮
    //                      ���� ���̺� ���� �����Ϸ��� �� �� ���ϰ� ����ϴ� �뵵

    //   2-3. Firebase    : �����ͺ��̽����� ������ ���� �����͸� �����մϴ�.(��Ƽ ����)

    //   2-4. ScriptableObject : ����Ƽ ���ο� Asset�� ���·ν� �����͸� �����ؼ� ����մϴ�.
    //                           �ΰ��� �����͸� ������ �� ���� ���� ���մϴ�.

    //   2-5. CSV         : ���� ���Ͽ� �ʿ��� �����͵��� �����صΰ�, C# ��ũ��Ʈ�� ���� �ش� ���� ���ͼ�
    //                      �����մϴ�. �ַ� �� ����, ��ũ��Ʈ ��ȭ ȣ��, �⺻���� ������

    public PlayerController playerController;

    private float score = 0.0f;

    //������ ���� ���̵� ǥ��
    private int level = 1;

    //�ִ� ����
    private int max_level = 10;

    //���� �� �䱸 ����
    private int levelperscore = 10;

    //�ؽ�Ʈ UI
    public TMP_Text scoreText;
    public TMP_Text levelText;    // ���� ����
    public TMP_Text perScoreText; // ���� ���������� ����
    public TMP_Text Player_Speed; // �÷��̾��� �̵� �ӵ�



    private void Start()
    {
        SetTMP_Text();
    }
    //String Format $
    //$"{����}"�� ���� ��� �ش� ������ ���ڿ��� �Ѿ�� �˴ϴ�.
    //{����:N0} : ���� ��� �� ,�� ǥ���� �� �ֽ��ϴ�. 1000 -> 1,000  
    // #,##0 : #�� 0�� �տ� �Ⱥ���. ���ڰ� ������ ǥ��, ������ ǥ�� ����
    //         0�� �տ� ����. ���ڰ� ������ ���� ǥ��, ������ 0 ǥ��


    void Update()
    {
        if(score >= levelperscore)
        {
            LevelUP();
        }

        score += 3 * Time.deltaTime;
        scoreText.text = ((int)score).ToString();    
    }

    private void LevelUP()
    {
        if (level == max_level)
            return;

        levelperscore *= 2;
        level++;
        playerController.SetSpeed(level);
        SetTMP_Text();
    }

    public void SetTMP_Text()
    {
        levelText.text = $"Level {level}";
        perScoreText.text = $"Goal : {levelperscore:N0}";
        Player_Speed.text = $"Speed : {playerController.GetSpeed()}";
    }
}
