using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueData", menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    [TextArea(2, 10)]
    public List<string> lines = new List<string>();

    [Tooltip("lines 리스트 순서와 동일한 순서로 이미지를 넣어주세요.")]
    public List<Sprite> images = new List<Sprite>();
}