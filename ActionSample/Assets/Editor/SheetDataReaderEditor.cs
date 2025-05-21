using GoogleSheetsToUnity;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

//커스텀 에디터 작성 이유
//스크립트에 대한 보조
//에디터 내에서 편하게 작업해야 하는 경우

#if UNITY_EDITOR
//해당 에디터는 SheetDataReader.cs에 대한 커스텀 데이터입니다.
[CustomEditor(typeof(SheetDataReader))]
public class SheetDataReaderEditor : Editor
{
    SheetDataReader sheetData;

    private void OnEnable()
    {
        sheetData = (SheetDataReader)target;
        //시트의 데이터는 에디터가 편집 중인 대상으로 처리합니다.
    }

    //인스펙터에 대한 제공
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); //기존 에디터의 설정을 그대로 사용합니다.

        //레이아웃에 글씨 작성
        GUILayout.Label("스프레드 시트 불러오기");

        //버튼 작성
        if(GUILayout.Button("데이터 읽기"))
        {
            //작업할 내용 구현
            DataRead(ButtonAction);     //데이터 읽기 작업 진행
            sheetData.dataList.Clear(); // 기존 데이터에 대한 초기화
        }
    }

    private void ButtonAction(GstuSpreadSheet arg0)
    {
        //시트 데이터에 적은 값만큼 작업을 진행합니다.
        for(int i = sheetData.row_length_begin; i <= sheetData.row_length_end; i++)
        {
            sheetData.UpdateSheetData(arg0.rows[i], i);
        }
        EditorUtility.SetDirty(target); //해당 오브젝트가 수정되었음을 유니티 에디터에 알리는 기능
    }

    //GSTU에서 제공해주는 SpreadSheet에 대한 유니티 이벤트를 전달받고, 작업을 진행합니다
    //1. 구글 스프레드 시트의 특정 시트와 워크 시트를 읽어오는 작업
    private void DataRead(UnityAction<GstuSpreadSheet> callback)
    {
        SpreadsheetManager.Read(new GSTU_Search(sheetData.address, sheetData.worksheet), callback);

        //SpreadsheetManager.Read()를 이용해 구글 시트에서 데이터를 받아올 수 있습니다(비동기)
        //GSTU_Search를 통해서 읽어낼 스프레드시트와 워크시트를 지정할 수 있습니다.
    }
}
#endif
