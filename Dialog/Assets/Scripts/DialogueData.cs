using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueData", menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    [TextArea(2, 10)]
    public List<string> lines = new List<string>();

    [Tooltip("lines ����Ʈ ������ ������ ������ �̹����� �־��ּ���.")]
    public List<Sprite> images = new List<Sprite>();
}