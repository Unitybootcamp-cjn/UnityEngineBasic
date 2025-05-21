using GoogleSheetsToUnity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text Text1;
    public Text Text2;
    public Text Text3;
    public Text Text4;
    public Text Text5;
    public Text Text6;
    GstuSpreadSheet arg0;
    List<GSTU_Cell> list;
    //각 행을 읽을 때 마다, 클래스 생성 후 리스트에 추가
    int Id = 0;
    string Name = null;
    string Description = null;
    int value = 0;
    int count = 0;
    int price = 0;

    private void Update()
    {
        //리스트의 개수만큼 작업을 진행합니다.
        for (int i = 0; i < list.Count; i++)
        {
            //현재 순서의 칼럼 아이디를 조사해서 그 값을 적용
            switch (list[i].columnId)
            {
                case "id":
                    Id = int.Parse(list[i].value);
                    Text1.text = Id.ToString();
                    break;
                case "name":
                    Name = list[i].value;
                    Text2.text = Name;
                    break;
                case "description":
                    Description = list[i].value;
                    Text3.text = Description;
                    break;
                case "value":
                    value = int.Parse(list[i].value);
                    Text4.text = value.ToString();
                    break;
                case "count":
                    count = int.Parse(list[i].value);
                    Text5.text = count.ToString();
                    break;
                case "price":
                    price = int.Parse(list[i].value);
                    Text6.text = price.ToString();
                    break;
            }
        }
    }
}
