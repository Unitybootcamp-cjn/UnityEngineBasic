using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Character; // 캐릭터
    public GameObject Platform; // 발판
    public Transform Platform_Parents; // 발판 모아줄 위치

    public int platform_pos_idx = 0; //발판 위치에 대한 인덱스 값
    public int character_pos_idx = 0; //캐릭터 위치에 대한 인덱스 값

    public bool isPlaying = false;

    //플랫폼 리스트(배치되어 있는 판)
    private List<GameObject> Platform_List = new List<GameObject>();

    //플랫폼에 대한 체크 리스트 (발판의 위치)
    private List<int> Platform_Check_List = new List<int>();

    private void Start()
    {
        SetFlatform(); //발판 설정
        Init(); //게임 초기화
    }

    private void Update()
    {
        //플레이 모드라면
        if(isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Check_Platform(character_pos_idx, 1);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Check_Platform(character_pos_idx, 0);
            }
        }
    }

    private void Check_Platform(int idx, int direction)
    {
        Debug.Log("인덱스 값 : " + idx + "/플랫폼 : " + Platform_Check_List[idx] + "/방향 : " + direction);

        //방향이 맞을 경우
        if (Platform_Check_List[idx % 20] == direction)
        {
            //캐릭터의 위치 변경
            character_pos_idx++;
            Character.transform.position = Platform_List[idx].transform.position + new Vector3(0f, 0.4f, 0f);

            //바닥 설정
            NextFlatform(platform_pos_idx);
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("게임 오버"); 
        isPlaying = false;
    }

    private void Init()
    {
        Character.transform.position = Vector3.zero;

        for (int pos_idx = 0; pos_idx < 20; pos_idx++)
        {
            NextFlatform(pos_idx);
        }

        character_pos_idx = 0;
        isPlaying = true;
    }

    private void NextFlatform(int idx)
    {
        int pos = UnityEngine.Random.Range(0, 2);

        if(idx == 0)
        {
            //Platform_Check_List[idx] = pos;
            Platform_List[idx].transform.position = new Vector3(0, -0.5f, 0);
        }
        else
        {
            if(platform_pos_idx < 20)
            {
                if (pos == 0)
                {
                    Platform_Check_List[idx] = pos;
                    Platform_List[idx].transform.position = Platform_List[idx - 1].transform.position + new Vector3(-1, 0.5f, 0);
                }
                else
                {
                    Platform_Check_List[idx] = pos;
                    Platform_List[idx].transform.position = Platform_List[idx - 1].transform.position + new Vector3(1, 0.5f, 0);
                }
            }
            else
            {
                //왼쪽 발판
                if (pos == 0)
                {
                    if(idx % 20 == 0)
                    {
                        Platform_Check_List[19] = pos;
                        Platform_List[idx % 20].transform.position = Platform_List[19].transform.position + new Vector3(-1, 0.5f, 0);
                    }
                    else
                    {
                        Platform_Check_List[idx % 20] = pos;
                        Platform_List[idx % 20].transform.position = Platform_List[idx % 20].transform.position + new Vector3(-1, 0.5f, 0);
                    }
                }
                //오른쪽 발판
                else
                {
                    if (idx % 20 == 0)
                    {
                        Platform_Check_List[19] = pos;
                        Platform_List[idx % 20].transform.position = Platform_List[19].transform.position + new Vector3(1, 0.5f, 0);
                    }
                    else
                    {
                        Platform_Check_List[idx % 20] = pos;
                        Platform_List[idx % 20].transform.position = Platform_List[idx % 20].transform.position + new Vector3(1, 0.5f, 0);
                    }
                }
            }
        }
        //platform_pos_idx++;
    }


    private void SetFlatform()
    {
        //임의의 숫자만큼 플랫폼 생성
        for(int i = 0; i < 20; i++)
        {
            GameObject plat = Instantiate(Platform, Vector3.zero, Quaternion.identity, Platform_Parents);
            Platform_List.Add(plat);
            Platform_Check_List.Add(0);
        }
    }
}
