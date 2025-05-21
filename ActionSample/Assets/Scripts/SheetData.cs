using UnityEngine;

public class SheetData : ScriptableObject
{
    [SerializeField] public string address;       //시트 주소
    [SerializeField] public string worksheet;     //워크 시트 연결
    [SerializeField] public int row_length_begin;  //시작 행 번호
    [SerializeField] public int row_length_end;   //마무리 행 번호
}
